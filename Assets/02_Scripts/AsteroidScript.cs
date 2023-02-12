using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidScript : MonoBehaviour
{
    public Transform hpTransform;
    public Transform hpBackTransform;
    private float speed = 6;
    public float rotSpeed = 5;
    public double hp = 10;
    public double coin = 2;
    public double maxHp;
    private Vector3 hpTargetScale;
    private Vector3 hpOrigin;
    
    float destroyTime = 0;
    bool destroyFlag = false;
    float destroyMaxTime = 0.3f;

    private void Awake()
    {
        hpOrigin = hpTransform.localPosition;
    }
    

    public void Init(double hp, double coin)
    {
        this.hp = hp;
        this.coin = coin;
        maxHp = hp;
        hpTargetScale = new Vector3(1, 1, 1);
        hpTransform.rotation = Quaternion.identity;
        hpBackTransform.rotation = Quaternion.identity;
        hpTransform.position = transform.position + hpOrigin;
        hpBackTransform.position = transform.position + hpOrigin;

        destroyTime = 0;
        destroyFlag = false;
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = true;
    }

    void Update()
    {
        //transform.position += Vector3.left * Time.deltaTime * speed;
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotSpeed));
        
        if (hp < 0)
        {
            hp = 0;
        }
        double result = hp / maxHp;
        hpTargetScale = new Vector3((float)result, 1, 1);
        hpTransform.transform.localScale =
            Vector3.Lerp(hpTransform.transform.localScale, hpTargetScale, Time.deltaTime * 3);
        hpTransform.rotation = Quaternion.identity;
        hpBackTransform.rotation = Quaternion.identity;
        hpTransform.position = transform.position + hpOrigin;
        hpBackTransform.position = transform.position + hpOrigin;
        if (destroyFlag)
        {
            destroyTime += Time.deltaTime;
            if (destroyTime > destroyMaxTime)
            {
                destroyFlag = false;
                ObjectPoolManager.instance.asteroid.Destroy(gameObject);
                
                GameObject explosionObject = ObjectPoolManager.instance.explosion.Create();
                explosionObject.transform.position = transform.position;
                explosionObject.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObject.GetComponent<ExplosionScript>();
                explosionScript.InitTime();

                string str = Util.GetBigNumber(maxHp);
                GameManager.instance.CreateFloatingText(str,transform.position);
                
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
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
        if (type == 0)
        {
            ObjectPoolManager.instance.asteroid.Destroy(gameObject);
        }
        else
        {
            destroyFlag = true;
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
        }
    }
}
