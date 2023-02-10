using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float time = 0;
    public float maxTime = 1;

    public void InitTime()
    {
        time = 0;
    }
    
    void Update()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            DestoryGameObject();
        }
    }

    public void DestoryGameObject()
    {
        ObjectPoolManager.instance.explosion.Destroy(gameObject);
    }
}
