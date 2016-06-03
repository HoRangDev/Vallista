using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer;
    private Transform _Transform;
    private AudioSource _AudioSource;

    [SerializeField]
    private Sprite[] _LevelSpriteList = new Sprite[3];

    [SerializeField]
    private List<AudioClip> _WallCollisionSoundEffects = new List<AudioClip>();
    private int _SoundEffectIndex = 0;

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
        _AudioSource = GetComponent(typeof(AudioSource)) as AudioSource;

        _CurrentDamage = _StartDamage;
    }

    public void SetToMove()
    {
        StartCoroutine("Movement");
    }

    IEnumerator Movement()
    {
        while (true)
        {
            Vector2 MoveVector = _DirectionVector * _Speed * Timer.Instance.DeltaTime;
            _Transform.position = new Vector2(_Transform.position.x + MoveVector.x, _Transform.position.y + MoveVector.y);
            yield return null;
        }
    }

    IEnumerator DestroyProcess()
    {
        while (true)
        {
            _ElasedTime += Timer.Instance.DeltaTime;
            if (_ElasedTime >= _DestroyTime)
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

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.CompareTag("Wall"))
        {
            _SoundEffectIndex = Mathf.Min(_SoundEffectIndex + 1, _WallCollisionSoundEffects.Count - 1);
            _AudioSource.clip = _WallCollisionSoundEffects[_SoundEffectIndex];
            _AudioSource.Play();
            _SpriteRenderer.sprite = _LevelSpriteList[Mathf.Clamp(_SoundEffectIndex, 0, 2)];
            _DirectionVector.x *= -1.0f;
            _CurrentDamage = Mathf.Clamp(_CurrentDamage * 2, _StartDamage, _MaxDamage);
        }
    }

    public void Destroy()
    {
        StopCoroutine("Movement");
        StartCoroutine("DestroyProcess");
    }
}
