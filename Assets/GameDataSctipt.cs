using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataSctipt : MonoBehaviour
{
    public static GameDataSctipt instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
