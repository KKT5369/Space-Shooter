using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScript : MonoBehaviour
{
    public float speed = 2;
    private SpriteRenderer spr;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        Vector3 pos = transform.position;
        if (pos.x + spr.bounds.size.x / 2 < -8)
        {
            float size = spr.bounds.size.x * 2;
            pos.x += size;
            transform.position = pos;
        }
    }
}
