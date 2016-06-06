using UnityEngine;
using System.Collections;

public class ArrowDirecting : MonoBehaviour
{
    private SpriteRenderer sprite = null;

	void Start ()
    {
        transform.position = GameManager.Instance.ProjectileSpawnPosition;
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.enabled = false;
	}
	
	void Update ()
    {
	    if(GameManager.Instance.IsOnTouch)
        {
            sprite.enabled = true;
            transform.localRotation = Quaternion.Euler(0, 0, 90 + GameManager.Instance.ProjectileAimAngle);
        }
        else
        {
            sprite.enabled = false;
        }
	}
}
