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
                asteroidScript.hp = 0;
                asteroidScript.DestroyGameObject(1);
                
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
                enemyScript.hp = 0;
                enemyScript.DestroyGameObject(1);
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
                bossScript.hp = 0;
                bossScript.DestroyGameObject(1);
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
