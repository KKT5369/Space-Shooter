using System;
using Game;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject item;
    public GameObject content;
    private int shipLength;

    private void Start()
    {
        shipLength = GameDataSctipt.instance.ships.Length;
        for (int i = 0; i < shipLength; i++)
        {
            ShipData ship = GameDataSctipt.instance.ships[i];
            GameObject obj = Instantiate(item, transform.position, quaternion.identity);
            MenuItemScript curItem = obj.GetComponent<MenuItemScript>();
            curItem.SetUI(ship.name, ship.chr_level.ToString(),ship.dmg.ToString(),ship.nextDmg.ToString());
            curItem.id = ship.id;
            obj.name = i.ToString();
            obj.transform.SetParent(content.transform, false);
            obj.SetActive(true);
            curItem.shipImage.sprite = Resources.Load<Sprite>(ship.GetImagName());
        }
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
