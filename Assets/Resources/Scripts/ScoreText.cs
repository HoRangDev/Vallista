using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text _Text = null;

	// Use this for initialization
	void Start ()
    {
        _Text = GetComponent<Text>();
        _Text.text = PlayerPrefs.GetInt("Score", 0).ToString();
	}
}
