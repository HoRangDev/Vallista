using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private Text _Text = null;

	void Awake ()
    {
        _Text = GetComponent<Text>();
        _Text.text = PlayerPrefs.GetInt("Score", 0).ToString();
	}
}
