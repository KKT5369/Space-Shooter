using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;
    public GameObject playerShotPrefab;
    public GameObject asteroidPrefab;
    public List<GameObject> enemyPrefab;
    public GameObject enemyShotPrefab;
    public GameObject coinPrefab;
    public GameObject shotEffectPrefab;
    public GameObject explosionPrefab;

    public ObjectPool playerShot;
    public ObjectPool asteroid;
    public List<ObjectPool> enemies;
    public ObjectPool enemyShot;
    public ObjectPool coin;
    public ObjectPool shotEffect;
    public ObjectPool explosion;
    
    public class ObjectPool
    {
        private List<GameObject> list;
        public GameObject prefab;

        public ObjectPool(GameObject prefab)
        {
            this.prefab = prefab;
            list = new List<GameObject>();
        }

        public GameObject Create()
        {
            if (list.Count > 0)
            {
                GameObject obj = list[0];
                obj.SetActive(true);
                list.Remove(obj);
                return obj;
            }
            else
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(true);
                return obj;
            }
        }

        public void Destroy(GameObject obj)
        {
            obj.SetActive(false);
            list.Add(obj);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerShot = new ObjectPool(playerShotPrefab);
        asteroid = new ObjectPool(asteroidPrefab);
        enemies = new List<ObjectPool>();
        for (int i = 0; i < enemyPrefab.Count; i++)
        {
            ObjectPool pool = new ObjectPool(enemyPrefab[i]);
            enemies.Add(pool);
        }

        enemyShot = new ObjectPool(enemyShotPrefab);
        coin = new ObjectPool(coinPrefab);
        shotEffect = new ObjectPool(shotEffectPrefab);
        explosion = new ObjectPool(explosionPrefab);
    }

    void Update()
    {
        
    }
}
