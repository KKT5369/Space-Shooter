using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject enemyShot;
    public float maxShotTime;
    public float shotSpeed;
    public float time;
    public int type = 0;
    public int hp = 3;
    public float speed = 4;
    public float coin = 0;
    
    void Start()
    {
        switch (type)
        {
            case 0:
                hp = 10; speed = 1.5f; coin = 3;
                maxShotTime = 3;
                shotSpeed = 3;
                break;
            case 1:
                hp = 20; speed = 1.4f; coin = 4;
                maxShotTime = 2;
                shotSpeed = 4;
                break;
            case 2:
                hp = 50; speed = 1.3f; coin = 5;
                maxShotTime = 1;
                shotSpeed = 5;
                break;
        }
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > maxShotTime)
        {
            GameObject shotObj =  Instantiate(enemyShot, transform.position, quaternion.identity);
            EnemyShotScript shotScript = shotObj.GetComponent<EnemyShotScript>();
            shotScript.speed = shotSpeed;
            time = 0;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
