using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestScoreText : MonoBehaviour {
    
	void Awake ()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("BestScore", 0).ToString();
	}
}
