using System;
using System.Collections;
using System.Collections.Generic;
using Game;
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
    public double dmg;
    public SpriteRenderer spr;
    public string horizontalAxis = "Horizontal";
    public string VerricalAxis = "Vertical";

    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //min = new Vector3(-8, -4.5f, 0);
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        //max = new Vector3(8, 4.5f, 0);
        colSize = GetComponent<BoxCollider2D>().size;
        chrSize = new Vector2(colSize.x / 2, colSize.y / 2);
        int select = GameDataSctipt.instance.select;
        ShipData shipData = GameDataSctipt.instance.ships[select];
        dmg = shipData.dmg;
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = Resources.Load<Sprite>(shipData.GetImagName());
    }

    void Update()
    {
        Move();
        PlayerShot();
    }

    public float shotMax = 0.5f;
    private float shotDelay;
    private void PlayerShot()
    {
        shotDelay += Time.deltaTime;
        if (shotDelay > shotMax)
        {
            shotDelay = 0;
            Vector3 vec = new Vector3(transform.position.x + 1.12f, 
                transform.position.y - 0.17f, transform.position.z);
            Instantiate(shot, vec , Quaternion.identity);
            //GameObject shotObj = Instantiate(shot, vec, quaternion.identity);
            GameObject shotObj = ObjectPoolManager.instance.playerShot.Create();
            shotObj.transform.position = vec;
            shotObj.transform.rotation = Quaternion.identity;
            ShotScript shotScript = shotObj.GetComponent<ShotScript>();
            shotScript.dmg = dmg;
            AudioManager.instance.PlaySound(Sound.PlayerShot);
        }
    }

    void Move()
    {
        float newX;
        float newY;
        if (Application.platform == RuntimePlatform.Android)
        {
            float x = SimpleInput.GetAxisRaw(horizontalAxis);
            float y = SimpleInput.GetAxisRaw(VerricalAxis);
            Vector3 dir = new Vector3(x, y, 0).normalized;
            transform.position = transform.position + dir * Time.deltaTime * speed;
            newX = transform.position.x;
            newY = transform.position.y;
        }
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector3 dir = new Vector3(x, y, 0).normalized;
            transform.position = transform.position + dir * Time.deltaTime * speed;
            newX = transform.position.x;
            newY = transform.position.y;
        }
        
        newX = Mathf.Clamp(newX, min.x + chrSize.x, max.x - chrSize.x);
        newY = Mathf.Clamp(newY, min.y + chrSize.y, max.y - chrSize.y);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Item"))
        {
            CoinScript coinScript = col.GetComponent<CoinScript>();
            GameManager.instance.coinInGame += coinScript.coinSize;
            GameDataSctipt.instance.AddCoin(coinScript.coinSize);
            //GameManager.instance.coinText.text = GameManager.instance.coinInGame.ToString();
            GameManager.instance.coinText.text = GameDataSctipt.instance.GetCoin().ToString();
            //Destroy(col.gameObject);
            coinScript.DestroyGameObject();
            AudioManager.instance.PlaySound(Sound.Coin);
        }
        else if (col.tag.Equals("Asteroid"))
        {
            GameManager.instance.isAlive = false;
            Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
            ObjectPoolManager.instance.asteroid.Destroy(col.gameObject);
            //Destroy(col.gameObject);
            Destroy(gameObject);
            //GameManager.instance.retryPanel.SetActive(true);
            GameManager.instance.RetryPanelSetActiveAfter1Sec();
            AudioManager.instance.PlaySound(Sound.Explosion);
        }
        else if (col.tag.Equals("Enemy"))
        {
            GameManager.instance.isAlive = false;
            Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
            //Destroy(col.gameObject);
            EnemyScript enemyScript = col.GetComponent<EnemyScript>();
            enemyScript.DestroyGameObject();
            Destroy(gameObject);
            //GameManager.instance.retryPanel.SetActive(true);
            GameManager.instance.RetryPanelSetActiveAfter1Sec();
            AudioManager.instance.PlaySound(Sound.Explosion);
        }
        else if (col.tag.Equals("EnemyShot"))
        {
            GameManager.instance.isAlive = false;
            Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
            //Destroy(col.gameObject);
            EnemyShotScript enemyShotScript = col.GetComponent<EnemyShotScript>();
            enemyShotScript.DestroyGameObject();
            Destroy(gameObject);
            //GameManager.instance.retryPanel.SetActive(true);
            GameManager.instance.RetryPanelSetActiveAfter1Sec();
            AudioManager.instance.PlaySound(Sound.Explosion);
        }
        else if (col.tag.Equals("BossShot"))
        {
            GameManager.instance.isAlive = false;
            Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
            //Destroy(col.gameObject);
            BossShotSctipt bossShotSctipt = col.GetComponent<BossShotSctipt>();
            bossShotSctipt.DestroyGameObject();
            Destroy(gameObject);
            //GameManager.instance.retryPanel.SetActive(true);
            GameManager.instance.RetryPanelSetActiveAfter1Sec();
            AudioManager.instance.PlaySound(Sound.Explosion);
        }
        else if (col.tag.Equals("Boss"))
        {
            GameManager.instance.isAlive = false;
            Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
            //Destroy(col.gameObject);
            BossScript bossScript = col.GetComponent<BossScript>();
            bossScript.DestroyGameObject();
            Destroy(gameObject);
            //GameManager.instance.retryPanel.SetActive(true);
            GameManager.instance.RetryPanelSetActiveAfter1Sec();
            AudioManager.instance.PlaySound(Sound.Explosion);
        }
    }
}
