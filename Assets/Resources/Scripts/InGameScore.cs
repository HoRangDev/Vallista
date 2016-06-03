using UnityEngine;
using System.Collections;

public class InGameScore : MonoBehaviour {

    private NumberImageText _Text = null;

	// Use this for initialization
	void Start ()
    {
        _Text = GetComponent<NumberImageText>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        _Text.Text = GameManager.Instance.CurrentScore.ToString();
	}
}
