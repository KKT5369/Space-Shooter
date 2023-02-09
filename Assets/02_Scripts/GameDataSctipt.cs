using Game;
using UnityEngine;

public class GameDataSctipt : MonoBehaviour
{
    private TextAsset shipTextAsset;
    public static GameDataSctipt instance;
    public ShipData[] ships;
    public float coin;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        shipTextAsset = Resources.Load<TextAsset>("ship");
        string[] lines = shipTextAsset.text.Split('\n');
        ships = new ShipData[lines.Length - 2];
        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split('\t');
            int id = int.Parse(rows[0]);
            float base_dmg = float.Parse(rows[1]);
            string name = rows[2];
            string kName = rows[3];
            int chr_level = PlayerPrefs.GetInt("Chr_Locked" + i.ToString(), 1);
            int locked;
            if (i == 1)
            {
                locked = 0;
            }
            else
            {
                locked = PlayerPrefs.GetInt("Chr_Locked" + i.ToString(), 1);
            }

            ships[i - 1] = new ShipData(id, base_dmg, name, kName, chr_level, locked);
            ships[i - 1].SetDamage();
            /*
            ships[i - 1].id = int.Parse(rows[0]);
            ships[i - 1].base_dmg = float.Parse(rows[1]);
            ships[i - 1].name = rows[2];
            ships[i - 1].kName = rows[3];
            */
        }

        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].Show();
        }
    }

    public float GetCoin()
    {
        PlayerPrefs.GetFloat("TotalCoin", 0);
        return this.coin;
    }

    public void AddCoin(float coin)
    {
        this.coin = coin;
        PlayerPrefs.SetFloat("TotalCoin", this.coin);
    }
    
}
