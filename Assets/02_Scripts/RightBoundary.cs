using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBoundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerShot")
        {
            ShotScript shotScript = col.GetComponent<ShotScript>();
                col.GetComponent<ShotScript>();
                shotScript.DestroyGameObject();
        }
        
    }
}
