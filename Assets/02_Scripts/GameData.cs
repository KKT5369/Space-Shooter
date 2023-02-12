using System;
using UnityEngine;

namespace Game
{
    public delegate void OnClick();

    public class Util
    {
        public static void CreatePopup(string title, string detail, OnClick yesAction, OnClick noAction)
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/Popup");
            GameObject parent = GameObject.Find("Canvas");
            GameObject popupObj = GameObject.Instantiate(obj, parent.transform, false);
            PopupScript popupScript = popupObj.GetComponent<PopupScript>();
            popupScript.titleText.text = title;
            popupScript.detailText.text = detail;
            popupScript.SetYesListener(yesAction);
            popupScript.SetNoListener(noAction);
        }
        public static void CreatePopup(string title, string detail, OnClick yesAction)
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/PopupOkay");
            GameObject parent = GameObject.Find("Canvas");
            GameObject popupObj = GameObject.Instantiate(obj, parent.transform, false);
            PopupScript popupScript = popupObj.GetComponent<PopupScript>();
            popupScript.titleText.text = title;
            popupScript.detailText.text = detail;
            popupScript.SetYesListener(yesAction);
        }
        private static string[] digit = new string[]
        {
            "","k","m","g","t","p","e","z","y","A","B","C","D","E","F","G","H","I","J","K","L","M",
            "N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
        };
        public static string GetBigNumber(double number)
        {
            if (number < 1000)
            {
                return number.ToString();
            }
            double expNum;
            int powNum;
            string numStr = number.ToString("E");
            string[] parts = numStr.Split("+");
            if (parts.Length < 2)
            {
                return "";
            }
            else
            {
                string expPart = parts[0].Remove(parts[0].Length - 1);
                string powPart = parts[1];
                expNum = double.Parse(expPart);
                powNum = int.Parse(powPart);
                int index = powNum / 3;
                int multiple = powNum % 3;
                expNum = expNum * Mathf.Pow(10, multiple);
                string firstStr = string.Format("{0:n3}", expNum);
                string secondStr = digit[index];
                string result = string.Concat(firstStr, secondStr);
                return result;
            }
        }
    }
    public struct ShipData
    {
        public int id;
        public float base_dmg;
        public string name;
        public string kName;
        public int chr_level;
        public int locked;
        public float dmg;
        public float nextDmg;
        public float unlockCoin;
        public float upgradeCoin;
        public float base_upgradeCoin;

        public ShipData(int id,float base_dmg,string name,string kName,float unlockCoin,float base_upgradeCoin,
                        int chr_level = 1,int locked = 1,
                        float dmg = 1,float nextDmg = 1,float upgradeCoin = 100)
        {
            this.id = id;
            this.base_dmg = base_dmg;
            this.name = name;
            this.kName = kName;
            this.unlockCoin = unlockCoin;
            this.base_upgradeCoin = base_upgradeCoin;
            this.chr_level = chr_level;
            this.locked = locked;
            this.dmg = dmg;
            this.nextDmg = nextDmg;
            this.upgradeCoin = upgradeCoin;
        }

        public string GetImagName()
        {
            return $"Character/{id.ToString()}/0";
        }
        
        //chr_lever 추가시 이 함수를 꼭 실행
        public void SetDamage()
        {
            this.dmg = chr_level * base_dmg;
            this.nextDmg = (chr_level + 1) * base_dmg;
        }

        public void SetUpgradeCoin()
        {
            this.upgradeCoin = chr_level * base_upgradeCoin;
        }
        public void Show()
        {
            Debug.Log($"id : {id} base_dmg : {base_dmg} name : {name} kName : {kName} unlockCoin : {unlockCoin} base_upgradeCoin : {base_upgradeCoin}" +
                      $" chr_level : {chr_level} locked : {locked} dmg : {dmg} upgradeCoin : {upgradeCoin}");
        }

        public void SetLock(int locked)
        {
            if (id == 0)
            {
                locked = 0;
            }
            this.locked = locked;
            PlayerPrefs.SetInt("Chr_Locked" + id.ToString(),locked);
        }

        public int GetLock()
        {
            if (id == 0)
            {
                return 0;
            }
            else
            {
                this.locked = PlayerPrefs.GetInt("Chr_Locked" + id.ToString(), 1);
                return this.locked;
            }
        }

        public void AddChrLevel()
        {
            chr_level++;
            PlayerPrefs.SetInt("Chr_Level" + id.ToString() , chr_level);
            SetDamage();
            SetUpgradeCoin();
        }
    }

    [System.Serializable]
    public struct EnemyWave
    {
        public int stage;
        public int type;
        public float time;

        public EnemyWave(int stage, int type, float time)
        {
            this.stage = stage;
            this.type = type;
            this.time = time;
        }

        public void Show()
        {
            Debug.Log($"stage : {stage} type : {type} time : {time}");
        }
    }
    [System.Serializable]
    public struct Enemy
    {
        public int id;
        public string name;
        public float hp;
        public float speed;
        public float maxShotTime;
        public float shotSpeed;
        public float coin;

        public Enemy(int id, string name, float hp,float speed,float maxShotTime,float shotSpeed,float coin)
        {
            this.id = id;
            this.name = name;
            this.hp = hp;
            this.speed = speed;
            this.maxShotTime = maxShotTime;
            this.shotSpeed = shotSpeed;
            this.coin = coin;
        }
    }
}
