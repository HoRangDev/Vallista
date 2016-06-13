using UnityEngine;
using System.Collections;

public class InGameScore : MonoBehaviour {

    private NumberImageText _Text = null;

	void Awake ()
    {
        _Text = GetComponent<NumberImageText>();
	}
	
	void Update ()
    {
        _Text.Text = GameManager.Instance.CurrentScore.ToString();
	}
}
