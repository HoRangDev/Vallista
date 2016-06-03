using UnityEngine;
using System.Collections;

public class ShopComponent : MonoBehaviour
{
    private static ShopComponent _Instance = null;
    public static ShopComponent Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = FindObjectOfType<ShopComponent>();

            return _Instance;
        }
    }

    private int[] _Items = new int[4];
    public int[] Items { get { return _Items; } }

    [SerializeField]
    private int[] _Prices = new int[4];

    private int _Coins = 0;
    public int Coin { get { return _Coins; } }

	// Use this for initialization
	void Start ()
    {
        _Coins = PlayerPrefs.GetInt("Coin", 0);
        _Coins += PlayerPrefs.GetInt("StockCoin", 0);
	    for(int i = 0; i < 4; i++)
        {
            _Items[i] = PlayerPrefs.GetInt("shop" + i.ToString(), 0);
        }
	}

    public void BuyItem(int index)
    {
        _Items[index] = 1;
    }

    public void EquipItem(int index)
    {
        for(int i = 0; i < 4; i++)
        {
            if (_Items[i] == 2)
                _Items[i] = 1;
        }

        _Items[index] = 2;
    }
	

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Coin", _Coins);
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("shop" + i.ToString(), _Items[i]);
        }
    }
}
