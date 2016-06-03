using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _ManagerInstance;
    public static GameManager Instance { get { return _ManagerInstance; } }

    private int _CurrentScore;
    public int CurrentScore { get { return _CurrentScore; } }

    private int _StockCoin;
    public int StockCoin { get { return _StockCoin; } }

    private bool _bIsOnTouch;
    public bool IsOnTouch { get { return _bIsOnTouch; } }
    private float _ProjectileAimAngle;
    public float ProjectileAimAngle { get { return _ProjectileAimAngle; } }

    private Vector2 _TouchBeganPosition;

    [SerializeField]
    private Object TestProjTilePrefab;

    [SerializeField]
    private Vector2 _ProjectileSpawnPosition;

    [SerializeField]
    private float _ProjectileRespawnDelay;
    private Projectile _CurrentProjectile;

    void Awake()
    {
        _ManagerInstance = this;
        SpawnProjectile();
    }

    void Start()
    {
        StartCoroutine("DetectProjectileShoot");
    }

    IEnumerator DetectProjectileShoot()
    {
        if (_CurrentProjectile != null)
        {
            Touch CurrentTouch = Input.GetTouch(0);
            switch (CurrentTouch.phase)
            {
                case TouchPhase.Began:
                    _TouchBeganPosition = CurrentTouch.position;
                    _bIsOnTouch = true;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    Vector2 TouchEndPosition = CurrentTouch.position;
                    if (_TouchBeganPosition.y > TouchEndPosition.y)
                    {
                        _ProjectileAimAngle = Vector2.Angle(TouchEndPosition, _TouchBeganPosition);
                        Vector2 DeltaTouchPosition = TouchEndPosition - _TouchBeganPosition;
                        DeltaTouchPosition.Normalize();

                        _CurrentProjectile = null;
                        Invoke("SpawnProjectile", _ProjectileRespawnDelay);
                    }

                    _bIsOnTouch = false;
                    break;

            }
        }

        yield return null;
    }

    public void AddScore(int Amount)
    {
        _CurrentScore += Amount;
    }

    public void AddCoin(int Amount)
    {
        _StockCoin += Amount;
    }

    void SpawnProjectile()
    {
        GameObject ProjectileObj = Instantiate(TestProjTilePrefab) as GameObject;
        if (ProjectileObj != null)
        {
            ProjectileObj.transform.position = _ProjectileSpawnPosition;
            _CurrentProjectile = ProjectileObj.GetComponent(typeof(Projectile)) as Projectile;
            _CurrentProjectile.SetToMove();
        }
    }

}
