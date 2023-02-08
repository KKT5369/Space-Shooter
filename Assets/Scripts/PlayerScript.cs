using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject shot;

    public float speed = 8;
    private Vector3 min, max;
    private Vector2 colSize;
    private Vector2 chrSize;

    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //min = new Vector3(-8, -4.5f, 0);
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        //max = new Vector3(8, 4.5f, 0);
        colSize = GetComponent<BoxCollider2D>().size;
        chrSize = new Vector2(colSize.x / 2, colSize.y / 2);
        
    }

    void Update()
    {
        Move();
        PlayerShot();
    }

    public float shotMax = 0.2f;
    private float shotDelay;
    private void PlayerShot()
    {
        shotDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if (shotDelay > shotMax)
            {
                shotDelay = 0;
                Vector3 vec = new Vector3(transform.position.x + 1.12f, 
                    transform.position.y - 0.17f, transform.position.z);
                Instantiate(shot, vec , Quaternion.identity);
            }
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(x, y, 0).normalized;
        transform.position = transform.position + dir * Time.deltaTime * speed;
        float newX = transform.position.x;
        float newY = transform.position.y;
        /*
        if (newX < min.x + chrSize.x)
        {
            newX = min.x + chrSize.x;
        }
        if (newX > max.x - chrSize.x)
        {
            newX = max.x - chrSize.x;
        }
        if (newY < min.y + chrSize.y)
        {
            newY = min.y + chrSize.y;
        }
        if (newY > max.y - chrSize.y)
        {
            newY = max.y - chrSize.y;
        }
        */
        newX = Mathf.Clamp(newX, min.x + chrSize.x, max.x - chrSize.x);
        newY = Mathf.Clamp(newY, min.y + chrSize.y, max.y - chrSize.y);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Item"))
        {
            CoinScript coinScript = col.GetComponent<CoinScript>();
            GameManager.instance.coin += coinScript.coinSize;
            GameManager.instance.coinText.text = GameManager.instance.coin.ToString();
            Destroy(col.gameObject);
        }
        else if (col.tag.Equals("Asteroid") || col.tag.Equals("Enemy") || col.tag.Equals("EnemyShot"))
        {
            Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
