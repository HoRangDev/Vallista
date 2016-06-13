using UnityEngine;
using System.Collections;

public class InitShop : MonoBehaviour {

	void Awake ()
    {
        PlayerPrefs.SetInt("shop0", 2);
	}
}
