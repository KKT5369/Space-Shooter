using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private float speed = 6;
    public float rotSpeed = 5;
    public float hp = 10;
    public float coin = 2;
    [SerializeField] private GameObject ExplosionAnim;

    public void Init(float hp, float coin)
    {
        this.hp = hp;
        this.coin = coin;
    }

    void Update()
    {
        //transform.position += Vector3.left * Time.deltaTime * speed;
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        transform.Rotate(new Vector3(0,0, Time.deltaTime * rotSpeed));
    }

    public void DestroyGameObject()
    {
        //GameManager.instance.remainEnemy--;
        //Destroy(gameObject);
        ObjectPoolManager.instance.asteroid.Destroy(gameObject);
        
    }
}
