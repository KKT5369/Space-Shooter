using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyShot;
    public Transform hpTransform;
    public float maxShotTime;
    public float shotSpeed;
    public float time;
    public int type = 0;
    public float hp = 3;
    public float speed = 4;
    public float coin = 0;
    public string enemyName;
    public float maxHp;
    private Vector3 hpTargetScale;

    public void Init(int type, string name, float hp, float speed, float maxShotTime, float shotSpeed, float coin)
    {
        this.type = type;
        this.enemyName = name;
        this.hp = hp;
        this.speed = speed;
        this.maxShotTime = maxShotTime;
        this.shotSpeed = shotSpeed;
        this.coin = coin;
        maxHp = hp;
        hpTargetScale = new Vector3(1, 1, 1);
    }
    void Start()
    {
        maxHp = hp;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > maxShotTime)
        {
            //GameObject shotObj =  Instantiate(enemyShot, transform.position, quaternion.identity);
            GameObject shotObj = ObjectPoolManager.instance.enemyShot.Create();
            shotObj.transform.position = transform.position;
            shotObj.transform.rotation = Quaternion.identity;
            EnemyShotScript shotScript = shotObj.GetComponent<EnemyShotScript>();
            shotScript.speed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        hpTargetScale = new Vector3(hp / maxHp, 1, 1);
        hpTransform.transform.localScale = Vector3.Lerp(hpTransform.transform.localScale,hpTargetScale, Time.deltaTime * 3);
    }

    public void DestroyGameObject()
    {
        GameManager.instance.remainEnemy--;
        //Destroy(gameObject);
        ObjectPoolManager.instance.enemies[type].Destroy(gameObject);
    }
}
