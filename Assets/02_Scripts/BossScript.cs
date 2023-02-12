using System.Collections;
using System.Collections.Generic;
using Game;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossScript : MonoBehaviour
{
    public enum MoveType
    {
        START,
        MOVE,
        ATTACK,
        RANDOM_MOVE
    };

    public enum RandomMoveType
    {
        START,
        MOVE
    };
    public struct State
    {
        public Vector3 pos;
        public float time;
        public MoveType type;

        public State(Vector3 pos, float time, MoveType type)
        {
            this.pos = pos;
            this.time = time;
            this.type = type;
        }
    }
    
    public Transform shotTr;
    public Transform hpTransform;
    public float shotDelay;
    public float shotMax = 0.1f;
    public List<State> orders;
    public float time = 0;
    public int index;
    public double hp;
    public double coin;
    public double maxHp;
    public Vector3 hpTargetScale;
    private RandomMoveType randomMoveState = RandomMoveType.START;
    private Vector3 randomPos;
    
    float destroyTime = 0;
    bool destroyFlag = false;
    float destroyMaxTime = 0.3f;

    public void Init(double hp, double coin)
    {
        this.hp = hp;
        this.coin = coin;
        maxHp = hp;
        hpTargetScale = new Vector3(1, 1, 1);
        
        destroyTime = 0;
        destroyFlag = false;
        Collider2D col = GetComponent<Collider2D>();
        col.enabled = true;
    }
    void Start()
    {
        orders = new List<State>();
        time = 0;
        index = 0;
        orders.Add(new State(new Vector3(10,0,0),1,MoveType.START));
        orders.Add(new State(new Vector3(6,0,0),1,MoveType.MOVE));
        orders.Add(new State(new Vector3(6,0,0),1,MoveType.ATTACK));
        orders.Add(new State(new Vector3(6,3,0),1,MoveType.MOVE));
        orders.Add(new State(new Vector3(6,3,0),1,MoveType.ATTACK));
        orders.Add(new State(new Vector3(6,0,0),1,MoveType.MOVE));
        orders.Add(new State(new Vector3(6,0,0),1,MoveType.ATTACK));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.MOVE));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.ATTACK));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.RANDOM_MOVE));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.ATTACK));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.RANDOM_MOVE));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.ATTACK));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.RANDOM_MOVE));
        orders.Add(new State(new Vector3(6,-3,0),1,MoveType.ATTACK));
        maxHp = hp;
        hpTargetScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > orders[index].time)
        {
            time = 0;
            index++;
            if (index >= orders.Count)
            {
                index = 1;
            }
            shotDelay = 0;
            randomMoveState = RandomMoveType.START;
        }
        else
        {
            if (orders[index].type == MoveType.START)
            {
                transform.position = orders[index].pos;
            }
            else if(orders[index].type == MoveType.MOVE)
            {
                transform.position = Vector3.Lerp(transform.position, orders[index].pos, Time.deltaTime * 10);
            }
            else if(orders[index].type == MoveType.ATTACK)
            {
                shotDelay += Time.deltaTime;
                if (shotDelay > shotMax)
                {
                    GameObject shot = ObjectPoolManager.instance.bossShot.Create();
                    shot.transform.position = shotTr.position;
                    shot.transform.rotation = quaternion.identity;
                    shotDelay = 0;
                }
            }
            else if(orders[index].type == MoveType.RANDOM_MOVE)
            {
                if (randomMoveState == RandomMoveType.START)
                {
                    randomPos = new Vector3(orders[index].pos.x, Random.Range(-3.0f, 3.0f), orders[index].pos.z);
                    randomMoveState = RandomMoveType.MOVE;
                }
                transform.position = Vector3.Lerp(transform.position, randomPos, Time.deltaTime * 10);
            }
        }

        if (hp < 0)
        {
            hp = 0;
        }
        double result = hp / maxHp;
        hpTargetScale = new Vector3((float)result, 1, 1);
        hpTransform.transform.localScale = Vector3.Lerp(hpTransform.transform.localScale,hpTargetScale, Time.deltaTime * 3);
        
        if (destroyFlag == true)
        {
            destroyTime += Time.deltaTime;
            if (destroyTime > destroyMaxTime)
            {
                destroyFlag = false;
                Destroy(gameObject);
                //Destroy(Instantiate(explosion, transform.position, quaternion.identity),1f);
                GameObject explosionObject = ObjectPoolManager.instance.explosion.Create();
                explosionObject.transform.position = transform.position;
                explosionObject.transform.rotation = Quaternion.identity;
                ExplosionScript explosionScript = explosionObject.GetComponent<ExplosionScript>();
                explosionScript.InitTime();
                
                string str = Util.GetBigNumber(maxHp);
                GameManager.instance.CreateFloatingText(str,transform.position);
                
                Vector3 randomPos = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f),0);
                //GameObject coinObj = Instantiate(coin, transform.position + randomPos, Quaternion.identity);
                GameObject coinObj = ObjectPoolManager.instance.coin.Create();
                coinObj.transform.position = transform.position + randomPos;
                coinObj.transform.rotation = Quaternion.identity;
                CoinScript coinScript = coinObj.GetComponent<CoinScript>();
                coinScript.coinSize = coin;
                AudioManager.instance.PlaySound(Sound.Explosion);
            }
        }
    }
    
    public void DestroyGameObject(int type = 0)
    {
        GameManager.instance.remainEnemy--;
        if (type == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            destroyFlag = true;
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
        }
    }
}
