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
    private BoxCollider2D _Collider;
    private Transform _Transform;

    private AudioSource _AudioSource;

    [SerializeField]
    private AudioSource _SubAudioSource;

    [SerializeField]
    private Sprite[] _LevelSpriteList = new Sprite[3];

    [SerializeField]
    private List<AudioClip> _WallCollisionSoundEffects = new List<AudioClip>();
    private int _SoundEffectIndex = 0;

    [SerializeField]
    private AudioClip _HitSoundEffect;

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

    private bool _bIsValid = true;
    public bool IsValid { get { return _bIsValid; } }

    private uint _WallCount = 0;

    void Awake()
    {
        _bIsValid= true;
        _Transform = transform;
        _Collider = GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
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
            if(_Transform.position.y >= 1280.0f * 0.518f)
            {
                Destroy(gameObject);
            }
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
                StopAllCoroutines();
               GameObject.Destroy(gameObject);
            }
            else
            {
                float FadeOutRatio = Mathf.Lerp(1.0f, 0.0f, Mathf.Clamp01(_ElasedTime / _DestroyTime));
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
            if (_WallCount >= 4)
            {
                Destroy(false);
            }
            else
            {
                ++_WallCount;
                _SoundEffectIndex = Mathf.Min(_SoundEffectIndex + 1, _WallCollisionSoundEffects.Count - 1);
                _AudioSource.clip = _WallCollisionSoundEffects[_SoundEffectIndex];
                _AudioSource.Play();
                _SpriteRenderer.sprite = _LevelSpriteList[Mathf.Clamp(_SoundEffectIndex, 0, 2)];
                _DirectionVector.x *= -1.0f;
                _CurrentDamage = Mathf.Clamp(_CurrentDamage * 2, _StartDamage, _MaxDamage);

            }
        }
    }

    public void MonsterHit()
    {
        _Collider.enabled = false;
        _SubAudioSource.clip = _HitSoundEffect;
        _SubAudioSource.Play();
    }

    public void Destroy(bool IsStopMovement)
    {
        _bIsValid = false;

        if (IsStopMovement)
        {
            StopCoroutine("Movement");
        }
        StartCoroutine("DestroyProcess");
    }
}
