using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private float speed = 6;
    public float rotSpeed = 5;
    public int hp = 10;
    public float coin = 2;
    
    [SerializeField] private GameObject ExplosionAnim;

    void Update()
    {
        //transform.position += Vector3.left * Time.deltaTime * speed;
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        transform.Rotate(new Vector3(0,0, Time.deltaTime * rotSpeed));
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
