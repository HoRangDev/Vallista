using UnityEngine;
using System.Collections;

public class SendGoogleAnalisticsLevel : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GoogleAnalyticsV4.instance.LogEvent("GameStart", "Started", "Game Started", 1);
	}
}
