using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject bossObj;
    public List<GameObject> enemyWavePrefabs;
    public List<EnemyWave> enemyWaves;
    public int spawnIndex;
    public float spawnTime;
    public GameObject ClearPanel;
    public Text stageInClearText;
    public Text coinInClearText;
    public Button adButton;
    public Button nextStageButton;
    public GameObject coverPanel;
    public GameObject retryPanel;
    public Text stageInRetryText;
    public Text coinInRetryText;
    public Button retryButton;
    public Button mainMenuRetryButton;
    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject mainMenuButton;
    public Button pauseButton;
    public Text coinText;
    public static GameManager instance;
    public GameObject astroid;
    public List<GameObject> enemies;
    public float time = 0;
    public float coinInGame;
    public float maxRight;
    public int spawnCount = 0;
    public int spawnMax = 3;
    public int remainEnemy = 0;
    public bool stageClear = false;
    public int stageInGame;
    public bool isAlive = true;
    public bool bossStage = false;
    public bool bossSpwan = false;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        isAlive = true;
        stageInGame = GameDataSctipt.instance.GetStage();
        stageClear = false;
        coinInGame = 0;
        //coinText.text = coinInGame.ToString();
        coinText.text = GameDataSctipt.instance.GetCoin().ToString();
        maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        retryButton.onClick.AddListener(RetryAction);
        mainMenuRetryButton.onClick.AddListener(MainMenuInRetryAction);
        adButton.onClick.AddListener(AdAction);
        nextStageButton.onClick.AddListener(NextStageInClearAction);
        enemyWaves = new List<EnemyWave>();
        enemyWaves = GameDataSctipt.instance.GetStageWave(stageInGame);
        /*
        enemyWaves.Add(new EnemyWave(0,0,2));
        enemyWaves.Add(new EnemyWave(1,1,3));
        enemyWaves.Add(new EnemyWave(2,1,2));
        enemyWaves.Add(new EnemyWave(3,2,3));
        enemyWaves.Add(new EnemyWave(4,2,2));
        */
        remainEnemy = 0;
        spawnIndex = 0;
        spawnTime = 0;

        bossStage = false;
        bossSpwan = false;
        if (stageInGame % 5 == 0)
        {
            bossStage = true;
        }
        else
        {
            SpawnEnemyWave();
        }
    }

    public void SpawnEnemyWave()
    {
        int type = enemyWaves[spawnIndex].type;
        spawnTime += enemyWaves[spawnIndex].time;
        int count = enemyWavePrefabs[type].transform.childCount;
        //Instantiate(enemyWavePrefabs[type], new Vector3(maxRight + 2, Random.Range(-3.0f, 3.0f), 0), Quaternion.identity);
        Vector3 pos = new Vector3(0, Random.Range(-3.0f, 3.0f), 0);
        for (int i = 0; i < count; i++)
        {
            Transform tr = enemyWavePrefabs[type].transform.GetChild(i).transform;
            EnemyScript enemyPrefabScript = tr.GetComponent<EnemyScript>();
            int enemyType = enemyPrefabScript.type;
            GameObject enemyObj = ObjectPoolManager.instance.enemies[enemyType].Create();
            enemyObj.transform.position = tr.position + pos;
            enemyObj.transform.rotation = Quaternion.identity;
            EnemyScript enemyScript = enemyObj.GetComponent<EnemyScript>();
            Enemy enemy = GameDataSctipt.instance.enemies[enemyType];
            float cur_hp = GameDataSctipt.instance.GetEnemyHp(enemy.hp, stageInGame);
            float cur_coin = GameDataSctipt.instance.GetEnemyHp(enemy.coin, stageInGame);
            enemyScript.Init(enemyType,enemy.name,cur_hp,enemy.speed,enemy.maxShotTime,enemy.shotSpeed,cur_coin);
        }
        remainEnemy += count;
        spawnIndex++;
    }

    public float asteroidTime = 0;
    public float asteroidSpawnTime = 3;
    void Update()
    {
        time += Time.deltaTime;
        asteroidTime += Time.deltaTime;
        if (time > spawnTime)
        {
            if (bossStage == true)
            {
                if (bossSpwan == false)
                {
                    remainEnemy++;
                    GameObject boss = Instantiate(bossObj, new Vector3(10, 0, 0), Quaternion.identity);
                    BossScript bossScript = boss.GetComponent<BossScript>();
                    float hp = GameDataSctipt.instance.GetBossHp(stageInGame);
                    float coin = GameDataSctipt.instance.GetBossCoin(stageInGame);
                    bossScript.Init(hp,coin);
                    bossSpwan = true;
                }
                if (remainEnemy <= 0 && stageClear == false && isAlive)
                {
                    stageClear = true;
                    ClearPanelActiveAfter1Sec();
                }
            }
            else if (spawnIndex < enemyWaves.Count)
            {
                SpawnEnemyWave();
            }
            else
            {
                if (remainEnemy <= 0 && stageClear == false && isAlive)
                {
                    stageClear = true;
                    ClearPanelActiveAfter1Sec();
                }
            }
        }
        else if (asteroidTime > asteroidSpawnTime && spawnIndex < enemyWaves.Count)
        {
            GameObject obj = ObjectPoolManager.instance.asteroid.Create();
            obj.transform.position = new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0);
            obj.transform.rotation = Quaternion.identity;
            AsteroidScript asteroidScript = obj.GetComponent<AsteroidScript>();
            float hp = GameDataSctipt.instance.GetAsteroidHp(stageInGame);
            float coin = GameDataSctipt.instance.GetAsteroidCoin(stageInGame);
            asteroidScript.Init(hp,coin);
            asteroidTime = 0;
        }
        /*
        time += Time.deltaTime;
        if (time > spawnTime)
        {
            if (spawnCount >= spawnMax)
            {
                if (remainEnemy <= 0 && stageClear == false && isAlive)
                {
                    stageClear = true;
                    ClearPanelActiveAfter1Sec();
                }
            }
            else
            {
                int check = Random.Range(0, 2);
                if (check == 0)
                {
                    //Instantiate(astroid, new Vector3(maxRight + 2, Random.Range(-4.0f,4.0f) , 0), Quaternion.identity);
                    GameObject obj = ObjectPoolManager.instance.asteroid.Create();
                    obj.transform.position = new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0);
                    obj.transform.rotation = Quaternion.identity;
                }
                else
                {
                    int type = Random.Range(0, 3);
                    //Instantiate(enemies[type], new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0), Quaternion.identity);
                    GameObject obj = ObjectPoolManager.instance.enemies[type].Create();
                    obj.transform.position = new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0);
                    obj.transform.rotation = Quaternion.identity;
                }
                spawnCount++;
                remainEnemy++;
            }
            time = 0;
        }
        */
    }

    public void PauseAction()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ResumeAction()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    
    public void MainMenuAction()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        SceneManager.LoadScene("ManuScene");
    }
    
    void RetryAction()
    {
        retryPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    public void AdAction()
    {
        print("AdAcion");
    }
    public void NextStageInClearAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void MainMenuInRetryAction()
    {
        retryPanel.SetActive(false);
        SceneManager.LoadScene("ManuScene");
    }
    public void RetryPanelSetActiveAfter1Sec()
    {
        coverPanel.SetActive(true);
        Invoke("RetryPanelSetActive",1);
    }
    public void RetryPanelSetActive()
    {
        coverPanel.SetActive(false);
        stageInRetryText.text = stageInGame.ToString();
        coinInRetryText.text = coinInGame.ToString();
        retryPanel.SetActive(true);
    }
    
    public void ClearPanelActiveAfter1Sec()
    {
        coverPanel.SetActive(true);
        GameDataSctipt.instance.AddStage();
        Invoke("ClearPanelActive",1);
    }
    public void ClearPanelActive()
    {
        coverPanel.SetActive(false);
        stageInClearText.text = stageInGame.ToString();
        coinInClearText.text = coinInGame.ToString();
        ClearPanel.SetActive(true);
    }

    public void CreateFloatingText(string text, Vector3 pos)
    { 
        GameObject obj = ObjectPoolManager.instance.floatingText.Create();
        Transform canvasTr = GameObject.Find("Canvas").transform;
        obj.transform.SetParent(canvasTr, false);
        obj.transform.position = pos;
        obj.GetComponent<Text>().text = text;
        FloationgTextScript floationTextScript = obj.GetComponent<FloationgTextScript>();
        floationTextScript.Init();
    }

}
