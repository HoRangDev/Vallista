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
            {
                _Instance = FindObjectOfType<ShopComponent>();
                _Instance.Start();
            }

            return _Instance;
        }
    }

    [SerializeField]
    private CoinText _CoinText;

    private int[] _Items = new int[4];
    public int[] Items { get { return _Items; } }

    private int _Coins = 0;
    public int Coin { get { return _Coins; } }

    private ShoppingItems[] _ShopItems = new ShoppingItems[4];

    private bool _Initialized = false;

	void Start ()
    {
        if(!_Initialized)
        {
            _Initialized = true;
            _Coins = PlayerPrefs.GetInt("Coin", 0);
            _Coins += PlayerPrefs.GetInt("StockCoin", 0);
            for (int i = 0; i < 4; i++)
            {
                if (i == 0 && _Items[0] == 0)
                {
                    _Items[i] = PlayerPrefs.GetInt("shop" + i.ToString(), 2);
                }
                else
                {
                    _Items[i] = PlayerPrefs.GetInt("shop" + i.ToString(), 0);

                }
            }

            _ShopItems = FindObjectsOfType<ShoppingItems>();
            _CoinText.Refresh();
        }
	}

    public void BuyItem(int index)
    {
        if(_Items[index] == 0 && _Coins >= 200)
        {
            _Coins -= 200;
            _Items[index] = 1;
            _CoinText.Refresh();
        }
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
	
    public void UpdateShopItems()
    {
        for(int i = 0; i < 4; i++)
        {
            _ShopItems[i].Refresh();
        }
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Coin", _Coins);
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt("shop" + i.ToString(), _Items[i]);

            if (_Items[i] == 2)
                PlayerPrefs.SetInt("EquipedBall", i);
        }
    }
}
