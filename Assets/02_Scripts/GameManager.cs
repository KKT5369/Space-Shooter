using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    public float spawnTime = 2;
    public float coinInGame;
    public float maxRight;
    public int spawnCount = 0;
    public int spawnMax = 3;
    public int remainEnemy = 0;
    public bool stageClear = false;
    public int stageInGame;
    public bool isAlive = true;
    
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
        
    }

    void Update()
    {
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

}
