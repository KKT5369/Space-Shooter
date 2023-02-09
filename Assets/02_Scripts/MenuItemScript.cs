using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    public Button unlockButton;
    public int id;
    public Text shipNameText;
    public Text LevelText;
    public Text dmgText;
    public Text nextDmgText;
    public Image shipImage;
    public Text UnlockCoinText;
    

    public void SetUI(string shipName,string shipLevel,string shipDmg,string shipNextDmg,int locked,float unlockCoin)
    {
        this.shipNameText.text = shipName;
        this.LevelText.text = shipLevel;
        this.dmgText.text = shipDmg;
        this.nextDmgText.text = shipNextDmg + " Coin";
        this.UnlockCoinText.text = unlockCoin.ToString();
        if (locked == 1)
        {
            unlockButton.gameObject.SetActive(true);
            UnlockCoinText.gameObject.SetActive(true);
        }
        else
        {
            unlockButton.gameObject.SetActive(false);
            UnlockCoinText.gameObject.SetActive(false);
        }
    }

    public void UnlockAction()
    {
        if (GameDataSctipt.instance.CanUnlock(id))
        {
            GameDataSctipt.instance.ExcuteUnlock(id);
            unlockButton.gameObject.SetActive(false);
            UnlockCoinText.gameObject.SetActive(false);
            MenuManager.instance.coinText.text = GameDataSctipt.instance.GetCoin().ToString();
        }
    }

    public void PowerUpAction()
    {
        print("PowerUpAction");
    }
}
