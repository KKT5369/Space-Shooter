using System.Collections.Generic;
using Game;
using UnityEngine;

public class GameDataSctipt : MonoBehaviour
{
    private TextAsset shipTextAsset;
    private TextAsset enemyWaveTextAsset;
    private TextAsset enemyTextAsset;
    public static GameDataSctipt instance;
    public ShipData[] ships;
    public EnemyWave[] enemyWaves;
    public Enemy[] enemies;
    public float coin;
    private int _select;
    private int stage;

    public int GetStage()
    {
        stage = PlayerPrefs.GetInt("Stage", 1);
        return stage;
    }

    public void AddStage()
    {
        stage = PlayerPrefs.GetInt("Stage",1);
        stage++;
        PlayerPrefs.SetInt("Stage", stage);
    }

    public int select
    {
        get
        {
            _select = PlayerPrefs.GetInt("ChrSelect", 0);
            return _select;
        }
        set
        {
            _select = value;
            PlayerPrefs.SetInt("ChrSelect",_select);
        }
    }

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

    private void Start()
    {
        shipTextAsset = Resources.Load<TextAsset>("ship");
        string[] lines = shipTextAsset.text.Split('\n');
        ships = new ShipData[lines.Length - 2];
        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split('\t');
            int id = int.Parse(rows[0]);
            float base_dmg = float.Parse(rows[1]);
            string name = rows[2];
            string kName = rows[3];
            float unlockCoin = float.Parse(rows[4]);
            float baseUpgradeCoin = float.Parse(rows[5]);
            int chr_level = PlayerPrefs.GetInt("chr_level" + (i-1), 1);
            int locked;
            if (i == 1)
            {
                locked = 0;
            }
            else
            {
                locked = PlayerPrefs.GetInt("Chr_Locked" + (i - 1), 1);
            }

            ships[i - 1] = new ShipData(id, base_dmg, name, kName,unlockCoin,baseUpgradeCoin, chr_level, locked);
            ships[i - 1].SetDamage();
            ships[i - 1].SetUpgradeCoin();
            /*
            ships[i - 1].id = int.Parse(rows[0]);
            ships[i - 1].base_dmg = float.Parse(rows[1]);
            ships[i - 1].name = rows[2];
            ships[i - 1].kName = rows[3];
            */
        }

        // for (int i = 0; i < ships.Length; i++)
        // {
        //     ships[i].Show();
        // }
        LoadEnemy();
        LoadEnemyWave();
    }
    public void LoadEnemy()
    {
        enemyTextAsset = Resources.Load<TextAsset>("enemy");
        string[] lines = enemyTextAsset.text.Split('\n');
        enemies = new Enemy[lines.Length - 2];
        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split('\t');
            int type = int.Parse(rows[0]);
            string name = rows[1];
            float hp = float.Parse(rows[2]);
            float speed = float.Parse(rows[3]);
            float maxShotTime = float.Parse(rows[4]);
            float shotSpeed = float.Parse(rows[5]);
            float coin = float.Parse(rows[6]);
            
            enemies[i - 1] = new Enemy(type,name,hp,speed,maxShotTime,shotSpeed,coin);
        }
    }
    public void LoadEnemyWave()
    {
        enemyWaveTextAsset = Resources.Load<TextAsset>("enemyWave");
        string[] lines = enemyWaveTextAsset.text.Split('\n');
        enemyWaves = new EnemyWave[lines.Length - 2];
        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split('\t');
            int stage = int.Parse(rows[0]);
            int type = int.Parse(rows[1]);
            float time = int.Parse(rows[2]);
            
            enemyWaves[i - 1] = new EnemyWave(stage,type,time);
        }
    }
    
    public float GetCoin()
    {
        coin = PlayerPrefs.GetFloat("TotalCoin",0);
        return coin;
    }

    public void AddCoinInMenu(float coin)
    {
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
        MenuManager.instance.coinImage.gameObject.SetActive(true);
        MenuManager.instance.coinText.gameObject.SetActive(true);
    }
    
    public void AddCoin(float coin)
    {
        this.coin += coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
        
    }

    public bool CanUnlock(int id)
    {
        if (GetCoin() > ships[id].unlockCoin)
        {
            if (ships[id].GetLock() == 1)
            {
                return true;
            }
            else
            {
                print("코인은 있지만 락이 해제됨");
                return false;
            }
        }
        else
        {
            print("코인이 없는 경우");
            return false;
        }
    }

    public void ExcuteUnlock(int id)
    {
        AddCoinInMenu(-ships[id].unlockCoin);
        ships[id].SetLock(0);
    }

    public bool CanUpgrade(int id)
    {
        if (GetCoin() > ships[id].upgradeCoin)
            return true;
        else
            return false;
    }

    public void UpgradeAction(int id)
    {
        AddCoinInMenu(-ships[id].upgradeCoin);
        ships[id].AddChrLevel();
    }

    public List<EnemyWave> GetStageWave(int stage)
    {
        List<EnemyWave> list = new List<EnemyWave>();
        for (int i = 0; i < enemyWaves.Length; i++)
        {
            if (enemyWaves[i].stage == stage)
            {
                list.Add(enemyWaves[i]);
            }
        }

        if (list.Count == 0)
        {
            for (int i = 0; i < enemyWaves.Length; i++)
            {
                if (enemyWaves[i].stage >= 3)
                {
                    list.Add(enemyWaves[i]);
                }
            }
        }
        return list;
    }

    public float GetEnemyHp(float base_hp, int stage)
    {
        return base_hp * stage;
    }
    public float GetEnemyCoin(float base_coin, int stage)
    {
        return base_coin * stage;
    }
    
    public float GetAsteroidHp(int stage)
    {
        return 1 * stage;
    }
    public float GetAsteroidCoin(int stage)
    {
        return 2 * stage;
    }
}
