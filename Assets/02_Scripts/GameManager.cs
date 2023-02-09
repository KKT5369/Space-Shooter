using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject mainMenuButton;
    public Button pauseButton;
    public Text coinText;
    public static GameManager instance;
    public GameObject astroid;
    public List<GameObject> enemies;
    public float time = 0;
    public float maxTime = 2;
    [FormerlySerializedAs("coin")] public float coinInGame;
    public float maxRight;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        coinInGame = 0;
        //coinText.text = coinInGame.ToString();
        coinText.text = GameDataSctipt.instance.GetCoin().ToString();
        maxRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            int check = Random.Range(0, 2);
            if (check == 0)
            {
                Instantiate(astroid, new Vector3(maxRight + 2, Random.Range(-4.0f,4.0f) , 0), Quaternion.identity);
            }
            else
            {
                int type = Random.Range(0, 3);
                Instantiate(enemies[type], new Vector3(maxRight + 2, Random.Range(-4.0f, 4.0f), 0), Quaternion.identity);
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
        SceneManager.LoadScene("ManuScene");
    }
}
