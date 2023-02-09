using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    public Button unlockButton;
    public int id;

    public void UnlockAction()
    {
        print("UnlockAction");
        gameObject.SetActive(true);
    }

    public void PowerUpAction()
    {
        print("PowerUpAction");
    }
}
