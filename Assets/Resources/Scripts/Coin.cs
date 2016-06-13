using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    private int _Amount;
    public int Amount { get { return _Amount; } set { _Amount = value; } }

    private SpriteRenderer _SpriteRenderer;
    private AudioSource _AudioSource;

    private bool _bIsCollectable = false;
    public bool IsCollectable { get { return _bIsCollectable; } }

    private float _ElasedTime;

    [SerializeField]
    private float _ActiveDelay = 1.0f;

    [SerializeField]
    private float _DestroyTime = 1.0f;

    [SerializeField]
    private int _AmountMin = 12;

    [SerializeField]
    private int _AmountMax = 20;

    void Awake()
    {
        _SpriteRenderer = GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        _AudioSource = GetComponent(typeof(AudioSource)) as AudioSource;
    }

    void Start()
    {
        _Amount = Random.Range(_AmountMin, _AmountMax);
        Invoke("SetToCollectable", _ActiveDelay);
    }

    void SetToCollectable()
    {
        _bIsCollectable = true;
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (_bIsCollectable)
        {
            if(Other.CompareTag("Projectile"))
            {
                Collected();
                GameManager.Instance.AddCoin(_Amount);
            }
        }
    }

    void Collected()
    {
        _bIsCollectable = false;
        _AudioSource.Play();
        StartCoroutine("DestroyProcess");
    }

    IEnumerator DestroyProcess()
    {
        while(true)
        {
            _ElasedTime += Time.deltaTime;
            if(_ElasedTime >= _DestroyTime)
            {
                Destroy(gameObject);
            }
            else
            {
                float FadeOutRatio = Mathf.Lerp(1.0f, 0.0f, (_ElasedTime / (_DestroyTime * 0.5f)));
                Color SpriteColor = _SpriteRenderer.color;
                SpriteColor.a = FadeOutRatio;
                _SpriteRenderer.color = SpriteColor;
            }
            yield return null;
        }
    }
}
