using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotScript : MonoBehaviour
{
    [SerializeField] private GameObject shotEffect;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject coin;
    public float speed = 10;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Asteroid"))
        {
            AsteroidScript asteroidScript = col.gameObject.GetComponent<AsteroidScript>();
            asteroidScript.hp -= 3;
            if (asteroidScript.hp <= 0)
            {
                Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = asteroidScript.coin;
                Destroy(col.gameObject);
            }
            Destroy(Instantiate(shotEffect, transform.position, Quaternion.identity),1f);
            Destroy(gameObject);
        }
        else if(col.tag.Equals("Enemy"))
        {
            EnemyScript enemyScript = col.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= 3;
            if (enemyScript.hp <= 0)
            {
                Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = enemyScript.coin;
                Destroy(col.gameObject);
            }
            Destroy(Instantiate(shotEffect, transform.position, Quaternion.identity),1f);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
