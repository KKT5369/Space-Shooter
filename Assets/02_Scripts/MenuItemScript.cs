using System.Collections;
using System.Collections.Generic;
using Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    public GameObject popupObj;
    public Button unlockButton;
    public Text UnlockCoinText;
    public int id;
    public Text shipNameText;
    public Text LevelText;
    public Text dmgText;
    public Text nextDmgText;
    public Image shipImage;
    public Text upgradeCoinText;
    

    public void SetUI(string shipName,string shipLevel,string shipDmg,string shipNextDmg,int locked,float unlockCoin,float upgradeCoin)
    {
        this.shipNameText.text = shipName;
        this.LevelText.text = shipLevel;
        this.dmgText.text = shipDmg;
        this.nextDmgText.text = shipNextDmg;
        this.UnlockCoinText.text = unlockCoin.ToString();
        this.upgradeCoinText.text = upgradeCoin + " Coins"; 
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
            string detailStr = GameDataSctipt.instance.ships[id].kName + " 을(를) 살꺼야?";
            Util.CreatePopup("구매",detailStr,ItemYesAction, (() => { }));
        }
        else
        {
            Util.CreatePopup("확인","코인이 부족 합니다.",(() => {}));
        }
    }
    

    void ItemYesAction()
    {
        GameDataSctipt.instance.ExcuteUnlock(id);
        unlockButton.gameObject.SetActive(false);
        UnlockCoinText.gameObject.SetActive(false);
        MenuManager.instance.coinText.text = GameDataSctipt.instance.GetCoin().ToString();
    }

    public void PowerUpAction()
    {
        if (GameDataSctipt.instance.CanUpgrade(id))
        {
            GameDataSctipt.instance.UpgradeAction(id);
            ShipData ship = GameDataSctipt.instance.ships[id];
            SetUI(ship.name, ship.chr_level.ToString(),ship.dmg.ToString(),ship.nextDmg.ToString(),ship.locked,ship.unlockCoin,ship.upgradeCoin);
            MenuManager.instance.coinText.text = GameDataSctipt.instance.GetCoin().ToString();
        }
        else
        {
            Util.CreatePopup("확인","코인이 부족 합니다.",(() => {}));
        }
    }
}
