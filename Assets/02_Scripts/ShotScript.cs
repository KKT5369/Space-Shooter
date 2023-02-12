using Game;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShotScript : MonoBehaviour
{
    [SerializeField] private GameObject shotEffect;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject coin;
    public float speed = 10;
    public double dmg;
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
            asteroidScript.hp -= dmg;
            GameObject shotGameObject = ObjectPoolManager.instance.shotEffect.Create();
            shotGameObject.transform.position = transform.position;
            shotGameObject.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotGameObject.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (asteroidScript.hp <= 0)
            {
                //Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
                GameObject explosionObject = ObjectPoolManager.instance.explosion.Create();
                explosionObject.transform.position = transform.position;
                explosionObject.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObject.GetComponent<ExplosionScript>();
                explosionScript.InitTime();

                string str = Util.GetBigNumber(asteroidScript.maxHp);
                GameManager.instance.CreateFloatingText(str,asteroidScript.transform.position);
                
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = asteroidScript.coin;
                Destroy(col.gameObject);
                asteroidScript.DestroyGameObject();
            }
            //Destroy(Instantiate(shotEffect, transform.position, Quaternion.identity),1f);
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        }
        else if(col.tag.Equals("Enemy"))
        {
            
            EnemyScript enemyScript = col.gameObject.GetComponent<EnemyScript>();
            enemyScript.hp -= dmg;
            GameObject shotGameObject = ObjectPoolManager.instance.shotEffect.Create();
            shotGameObject.transform.position = transform.position;
            shotGameObject.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotGameObject.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (enemyScript.hp <= 0)
            {
                //Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
                GameObject explosionObject = ObjectPoolManager.instance.explosion.Create();
                explosionObject.transform.position = transform.position;
                explosionObject.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObject.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                
                string str = Util.GetBigNumber(enemyScript.maxHp);
                GameManager.instance.CreateFloatingText(str,enemyScript.transform.position);
                
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = enemyScript.coin;
                //Destroy(col.gameObject);
                enemyScript.DestroyGameObject();
            }
            //Destroy(Instantiate(shotEffect, transform.position, Quaternion.identity),1f);
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        }
        else if(col.tag.Equals("Boss"))
        {
            
            BossScript bossScript = col.gameObject.GetComponent<BossScript>();
            bossScript.hp -= dmg;
            GameObject shotGameObject = ObjectPoolManager.instance.shotEffect.Create();
            shotGameObject.transform.position = transform.position;
            shotGameObject.transform.rotation = Quaternion.identity;
            ShotEffectScript shotEffectScript = shotGameObject.GetComponent<ShotEffectScript>();
            shotEffectScript.InitTime();
            if (bossScript.hp <= 0)
            {
                //Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
                GameObject explosionObject = ObjectPoolManager.instance.explosion.Create();
                explosionObject.transform.position = transform.position;
                explosionObject.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObject.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                
                string str = Util.GetBigNumber(bossScript.maxHp);
                GameManager.instance.CreateFloatingText(str,bossScript.transform.position);
                
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = bossScript.coin;
                //Destroy(col.gameObject);
                bossScript.DestroyGameObject();
            }
            //Destroy(Instantiate(shotEffect, transform.position, Quaternion.identity),1f);
            //Destroy(gameObject);
            ObjectPoolManager.instance.playerShot.Destroy(gameObject);
        }
    }

    public void DestroyGameObject()
    {
        ObjectPoolManager.instance.playerShot.Destroy(gameObject);
    }
}
