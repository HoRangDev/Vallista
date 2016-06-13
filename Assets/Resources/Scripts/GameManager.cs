using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private List<Object> _ProjectilePrefabList = new List<Object>();
    private int _SelectedProjectileIndex;

    private Object TestProjTilePrefab;
    
    [SerializeField]
    private Vector2 _ProjectileSpawnPosition;
    public Vector2 ProjectileSpawnPosition { get { return _ProjectileSpawnPosition; } }

    [SerializeField]
    private float _ProjectileRespawnDelay;
    private Projectile _CurrentProjectile;

    [SerializeField]
    private int _NeedScoreForLevelup;

    [SerializeField]
    ArrowDirecting _ArrowDirect;

    void Awake()
    {
        _ManagerInstance = this;
        _SelectedProjectileIndex = Mathf.Clamp(PlayerPrefs.GetInt("EquipedBall", 0), 0, _ProjectilePrefabList.Count - 1);
        SpawnProjectile();
    }

    void Start()
    {
        StartCoroutine("DetectProjectileShoot");
    }

    void LateUpdate()
    {
        if(_CurrentScore - (_NeedScoreForLevelup * MonsterManager.Instance.CurrentLevel) > _NeedScoreForLevelup)
        {
            MonsterManager.Instance.LevelUp();
        }
    }

    IEnumerator DetectProjectileShoot()
    {
        while (true)
        {
            if (_CurrentProjectile != null)
            {
                if (Input.touchCount != 0)
                {
                    Touch CurrentTouch = Input.GetTouch(0);

                    switch (CurrentTouch.phase)
                    {
                        case TouchPhase.Began:
                            _ArrowDirect.SetEnable(true);
                            _bIsOnTouch = true;
                            _TouchBeganPosition = CurrentTouch.position;
                            break;

                        case TouchPhase.Moved:
                            Vector2 MovedPosition = CurrentTouch.position;
                            Vector2 MovedDeltaPosition = MovedPosition - _TouchBeganPosition;
                            _ProjectileAimAngle = Mathf.Atan2(MovedDeltaPosition.y, MovedDeltaPosition.x) * Mathf.Rad2Deg;
                            _ArrowDirect.UpdateDirect();
                            break;


                        case TouchPhase.Ended:
                        case TouchPhase.Canceled:
                            _ArrowDirect.SetEnable(false);
                            _bIsOnTouch = false;
                            Vector2 TouchEndPosition = CurrentTouch.position;
                            Vector2 DeltaTouchPosition = -(TouchEndPosition - _TouchBeganPosition);
                            DeltaTouchPosition.Normalize();

                            _ProjectileAimAngle = Mathf.Atan2(DeltaTouchPosition.y, DeltaTouchPosition.x) * Mathf.Rad2Deg;
                            if (_TouchBeganPosition.y > TouchEndPosition.y)
                            {
                                _CurrentProjectile.DirectionVector = DeltaTouchPosition;
                                _CurrentProjectile.SetToMove();

                                _CurrentProjectile = null;
                                Invoke("SpawnProjectile", _ProjectileRespawnDelay);
                            }

                            break;
                    }
                }
            }

            yield return null;
        }
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
        GameObject ProjectileObj = Instantiate(_ProjectilePrefabList[_SelectedProjectileIndex]) as GameObject;
        if (ProjectileObj != null)
        {
            ProjectileObj.transform.position = _ProjectileSpawnPosition;
            _CurrentProjectile = ProjectileObj.GetComponent(typeof(Projectile)) as Projectile;
        }
    }

    public void SetToGameEnd()
    {
        PlayerPrefs.SetInt("StockCoin", _StockCoin);
        if(PlayerPrefs.GetInt("BestScore", 0) < _CurrentScore)
        {
            PlayerPrefs.SetInt("BestScore", _CurrentScore);
        }
        PlayerPrefs.SetInt("Score", _CurrentScore);
        ParkJunHo.SceneComponent.Instance.ScheduleLoadScene(0.1f, "ResultScene");
    }

}
