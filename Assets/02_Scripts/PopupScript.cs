using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

public class PopupScript : MonoBehaviour
{
    public Text titleText;
    public Text detailText;
    public Button yesButton;
    public Button noButton;

    private event OnClick onYesClick;
    private event OnClick onNoClick;
    public void SetYesListener(OnClick onClick){
        this.onYesClick = onClick;
    }
    public void SetNoListener(OnClick onClick){
        this.onNoClick = onClick;
    }
    void Start(){
        yesButton.onClick.AddListener(PopupYesAction);
        noButton.onClick.AddListener(PopupNoAction);
    }
    void PopupYesAction(){
        onYesClick();
        Destroy(gameObject);
    }
    void PopupNoAction()
    {
        onNoClick();
        Destroy(gameObject);
    }

}
