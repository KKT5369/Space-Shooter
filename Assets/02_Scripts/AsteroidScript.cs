using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

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


    private void Start()
    {
        string str = Util.GetBigNumber(1001111);
        print(str);
        maxHp = hp;
        hpTransform.rotation = Quaternion.identity;
        hpBackTransform.rotation = Quaternion.identity;
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
    }

    void Update()
    {
        //transform.position += Vector3.left * Time.deltaTime * speed;
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        transform.Rotate(new Vector3(0,0, Time.deltaTime * rotSpeed));

        double result = hp / maxHp;
        hpTargetScale = new Vector3((float)result, 1, 1);
        hpTransform.transform.localScale = Vector3.Lerp(hpTransform.transform.localScale,hpTargetScale, Time.deltaTime * 3);
        hpTransform.rotation = Quaternion.identity;
        hpBackTransform.rotation = Quaternion.identity;
        hpTransform.position = transform.position + hpOrigin;
        hpBackTransform.position = transform.position + hpOrigin;
    }

    public void DestroyGameObject()
    {
        //GameManager.instance.remainEnemy--;
        //Destroy(gameObject);
        ObjectPoolManager.instance.asteroid.Destroy(gameObject);
        
    }
}
