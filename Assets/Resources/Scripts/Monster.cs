using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Monster : MonoBehaviour
{
    SpriteRenderer _SpriteRenderer;

    [SerializeField]
    private float _ShockDelay;

    [SerializeField]
    private int _StartHealth;
    private int _CurrentHealth;

    private AudioSource _AudioSource;

    private Animator _Animator;
    private float _ElasedTime = 0.0f;

    [SerializeField]
    private float _Speed;

    [SerializeField]
    private float _DestroyTime;

    private bool _IsDead;

    [SerializeField]
    private Object _CoinPrefab;

    [SerializeField]
    private Transform _HPBarTransform;

    void Awake()
    {
        _IsDead = false;
        _CurrentHealth = _StartHealth;
        _SpriteRenderer = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        _AudioSource = GetComponent(typeof(AudioSource)) as AudioSource;
        _Animator = GetComponent(typeof(Animator)) as Animator;
    }

    IEnumerator Movement()
    {
        while(true)
        {
            if (!_IsDead)
            {
                Vector2 Position = transform.position;
                Position.y -= (_Speed * Timer.Instance.DeltaTime);
                transform.position = Position;
            }
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (!_IsDead)
        {
            if (Other.gameObject.CompareTag("Projectile"))
            {
                Projectile TargetProjectile = Other.GetComponent(typeof(Projectile)) as Projectile;
                if (TargetProjectile != null)
                {
                    if (TargetProjectile.IsValid)
                    {
                        _CurrentHealth -= TargetProjectile.Damage;
                        TargetProjectile.Destroy();
                        _AudioSource.Play();
                        _Animator.SetBool("IsAttacked", true);
                        StopCoroutine("Movement");

                        if (_CurrentHealth <= 0)
                        {
                            _HPBarTransform.gameObject.SetActive(false);
                            Dead();
                        }
                        else
                        {
                            Vector3 Scale = _HPBarTransform.localScale;
                            Scale.x = 10.0f * (_CurrentHealth / (float)_StartHealth);
                            _HPBarTransform.localScale = Scale;
                            Attacked();
                        }
                    }
                }
            }
            else if (Other.gameObject.CompareTag("EndLine"))
            {
                GameManager.Instance.SetToGameEnd();
            }
        }
    }

    public void SetToMove()
    {
        _Animator.SetBool("IsAttacked", false);
        StartCoroutine("Movement");
    }

    void Attacked()
    {
        Invoke("SetToMove", _ShockDelay);
    }

    void DropCoin()
    {
        float Range = Random.Range(0.0f, 1.0f);
        if(Range >= 0.3f && Range <= 0.75f)
        {
            GameObject CoinObj = Instantiate(_CoinPrefab) as GameObject;
            CoinObj.transform.position = transform.position;
        }
    }

    void Dead()
    {
        DropCoin();
        _IsDead = true;
        GameManager.Instance.AddScore(_StartHealth);
        StartCoroutine("DestroyProcess");
    }

    IEnumerator DestroyProcess()
    {
        while(true)
        {
            if (!_AudioSource.isPlaying)
            {
                _ElasedTime += Timer.Instance.DeltaTime;
                if (_ElasedTime >= _DestroyTime)
                {
                    Destroy(gameObject);
                }
                else
                {
                    float FadeOutRatio = Mathf.Lerp(1.0f, 0.0f, (_ElasedTime / _DestroyTime));
                    Color SpriteColor = _SpriteRenderer.color;
                    SpriteColor.a = FadeOutRatio;
                    _SpriteRenderer.color = SpriteColor;
                }
            }

            yield return null;
        }
    }
}
