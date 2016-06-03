using UnityEngine;
using System.Collections;

public class ArrowDirecting : MonoBehaviour
{
    private SpriteRenderer sprite = null;
    private float _Radius = 1f;
    private Vector3 _OriPos;

	// Use this for initialization
	void Start ()
    {
        transform.position = GameManager.Instance.ProjectileSpawnPosition;
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        _OriPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(GameManager.Instance.IsOnTouch)
        {
            sprite.enabled = true;
            transform.localRotation = Quaternion.Euler(0, 0, -GameManager.Instance.ProjectileAimAngle);
        }
        else
        {
            sprite.enabled = false;
        }
	}
}
