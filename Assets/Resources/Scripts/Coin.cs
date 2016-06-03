using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    private int _Amount;
    public int Amount { get { return _Amount; } set { _Amount = value; } }

    private SpriteRenderer _SpriteRenderer;

    private float _ElasedTime;
    private float _DestroyTime = 0.08f;

    void Awake()
    {
        _SpriteRenderer = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
    }

    void Start()
    {
        _Amount = Random.Range(0, 5);
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        switch(Other.tag)
        {
            case "Projectile":
                GameManager.Instance.AddCoin(_Amount);
                Collected();
                break;
        }
    }

    void Collected()
    {
        StartCoroutine("DestroyProcess");
    }

    IEnumerator DestroyProcess()
    {
        while(true)
        {
            _ElasedTime += Timer.Instance.DeltaTime;
            if(_ElasedTime >= _DestroyTime )
            {
                Destroy(gameObject);
            }
            else
            {
                float FadeOutRatio = 1.0f - Mathf.Clamp01((_ElasedTime / _DestroyTime));
                Color SpriteColor = _SpriteRenderer.color;
                SpriteColor.a = FadeOutRatio;
                _SpriteRenderer.color = SpriteColor;
            }
            yield return null;
        }
    }
}
