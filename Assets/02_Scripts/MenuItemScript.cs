using System.Collections;
using System.Collections.Generic;
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

    public void SetUI(string shipName,string shipLevel,string shipDmg,string shipNextDmg)
    {
        this.shipNameText.text = shipName;
        this.LevelText.text = shipLevel;
        this.dmgText.text = shipDmg;
        this.nextDmgText.text = shipNextDmg;
    }

    public void UnlockAction()
    {
        print("UnlockAction");
        unlockButton.gameObject.SetActive(false);
    }

    public void PowerUpAction()
    {
        print("PowerUpAction");
    }
}
