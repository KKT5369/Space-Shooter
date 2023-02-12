using System;
using Game;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button StartButton;
    public static MenuManager instance;
    public GameObject item;
    public GameObject content;
    public GameObject addButtonObj;
    public GameObject clearButtonObj;
    public Text coinText;
    public Image coinImage;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartButton.onClick.AddListener(GoGameScene);
        int shipLength = GameDataSctipt.instance.ships.Length;
        for (int i = 0; i < shipLength; i++)
        {
            ShipData ship = GameDataSctipt.instance.ships[i];
            GameObject obj = Instantiate(item, transform.position, quaternion.identity);
            MenuItemScript curItem = obj.GetComponent<MenuItemScript>();
            curItem.SetUI(ship.name, ship.chr_level.ToString(),ship.dmg.ToString(),ship.nextDmg.ToString(),ship.locked,ship.unlockCoin,ship.upgradeCoin);
            curItem.id = ship.id;
            obj.name = i.ToString();
            obj.transform.SetParent(content.transform, false);
            obj.SetActive(true);
            curItem.shipImage.sprite = Resources.Load<Sprite>(ship.GetImagName());
            GetComponent<ScrollViewSnap>().item.Add(obj);
            AudioManager.instance.PlayMusic(Music.Menu);
        }

        if (GameDataSctipt.instance.GetCoin() == 0)
        {
            coinText.gameObject.SetActive(false);
            coinImage.gameObject.SetActive(false);
        }
        else
        {
            coinText.gameObject.SetActive(true);
            coinImage.gameObject.SetActive(true);
            coinText.text = GameDataSctipt.instance.GetCoin().ToString();
        }
    }

    public void AddTestCoin()
    {
        GameDataSctipt.instance.AddCoinInMenu(10000);
        coinText.text = GameDataSctipt.instance.GetCoin().ToString();
    }

    public void ClearPrefAction()
    {
        PlayerPrefs.DeleteAll();
    }

    public void GoGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
