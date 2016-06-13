using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StockText : MonoBehaviour
{
    private Text _Text = null;

	void Awake ()
    {
        _Text = GetComponent<Text>();
        _Text.text = PlayerPrefs.GetInt("StockCoin", 0).ToString();
	}
}
