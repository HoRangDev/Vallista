using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BestScoreText : MonoBehaviour {
    
	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("BestScore", 0).ToString();
	}
}
