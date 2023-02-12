using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyShot;
    public Transform hpTransform;
    public float maxShotTime;
    public float shotSpeed;
    public float time;
    public int type = 0;
    public double hp = 3;
    public float speed = 4;
    public double coin = 0;
    public string enemyName;
    public double maxHp;
    private Vector3 hpTargetScale;
    
    float destroyTime = 0;
    bool destroyFlag = false;
    float destroyMaxTime = 0.3f;

    public void Init(int type, string name, double hp, float speed, float maxShotTime, float shotSpeed, double coin)
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
        
        destroyTime = 0;
        destroyFlag = false;
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = true;
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
        if (hp < 0)
        {
            hp = 0;
        }
        double reuslt = hp / maxHp;
        hpTargetScale = new Vector3((float)reuslt, 1, 1);
        hpTransform.transform.localScale = Vector3.Lerp(hpTransform.transform.localScale,hpTargetScale, Time.deltaTime * 3);
        
        if (destroyFlag == true)
        {
            destroyTime += Time.deltaTime;
            if (destroyTime > destroyMaxTime)
            {
                destroyFlag = false;
                ObjectPoolManager.instance.enemies[type].Destroy(gameObject);
                
                GameObject explosionObject = ObjectPoolManager.instance.explosion.Create();
                explosionObject.transform.position = transform.position;
                explosionObject.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObject.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                
                string str = Util.GetBigNumber(maxHp);
                GameManager.instance.CreateFloatingText(str,transform.position);
                
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = coin;
                AudioManager.instance.PlaySound(Sound.Explosion);
                
            }
        }
    }
    


    public void DestroyGameObject(int type = 0)
    {
        GameManager.instance.remainEnemy--;
        if (type == 0)
        {
            ObjectPoolManager.instance.enemies[type].Destroy(gameObject);
        }
        else
        {
            
            destroyFlag = true;
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
        }
    }
}
