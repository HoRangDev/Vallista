using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    Text _Text;

	void Awake()
    {
        _Text = GetComponent(typeof(Text)) as Text;
	}

    public void Refresh()
    {
        _Text.text = ShopComponent.Instance.Coin.ToString();
    }
}
