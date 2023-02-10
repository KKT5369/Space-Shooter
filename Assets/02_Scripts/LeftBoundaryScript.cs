using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftBoundaryScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Asteroid")
        {
            AsteroidScript asyncOperation = col.GetComponent<AsteroidScript>();
            asyncOperation.DestroyGameObject();;
        }
        else if (col.tag == "Enemy")
        {
            EnemyScript enemyScript = col.GetComponent<EnemyScript>();
            enemyScript.DestroyGameObject();;
        }
    }
}
