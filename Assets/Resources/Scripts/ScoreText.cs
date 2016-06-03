using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour
{
    public int _Score = 0;
    private GUIText _Text = null;

	// Use this for initialization
	void Start ()
    {
        _Text = GetComponent<GUIText>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _Text.text = _Score.ToString();
	}
}
