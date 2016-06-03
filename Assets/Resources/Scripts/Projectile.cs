using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer;
    private Transform _Transform;

    [SerializeField]
    private int _StartDamage;
    [SerializeField]
    private int _MaxDamage;
    private int _CurrentDamage;
    public int Damage { get { return _CurrentDamage; } }

    [SerializeField]
    private float _Speed;
    private Vector2 _DirectionVector;
    public Vector2 DirectionVector { get { return _DirectionVector; } set { _DirectionVector = value; } }

    [SerializeField]
    private float _DestroyTime;
    private float _ElasedTime;

    void Awake()
    {
        _Transform = transform;
        _SpriteRenderer = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        _CurrentDamage = _StartDamage;
    }

    public void SetToMove()
    {
        StartCoroutine("ProjectileMovement");
    }

    IEnumerator ProjectileMovement()
    {
        Vector2 MoveVector = _DirectionVector * _Speed * Timer.Instance.DeltaTime;
        _Transform.position = new Vector2(_Transform.position.x + MoveVector.x, _Transform.position.y + MoveVector.y);
        yield return null;
    }

    IEnumerator ProjectileDestroy()
    {
        _ElasedTime += Timer.Instance.DeltaTime;
        if(_ElasedTime >= _DestroyTime)
        {
            StopAllCoroutines();
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

    void OnTriggerEnter2D(Collider2D Other)
    {
        switch(Other.tag)
        {
            case "Wall":
                _DirectionVector.x *= -1.0f;
                _CurrentDamage = Mathf.Clamp(_CurrentDamage * 2, _StartDamage, _MaxDamage);
                break;
        }
    }

    public void Destroy()
    {
        StopCoroutine("ProjectileMovement");
        StartCoroutine("ProjectileDestroy");
    }
}
