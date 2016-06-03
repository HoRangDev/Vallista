using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager _MangerInstance;
    public static MonsterManager Instance { get { return _MangerInstance; } }

    // Ratio 0.0 ~ 1.0
    [SerializeField]
    private Vector2 _SpawnXPositionRatioRng;
    [SerializeField]
    // Ratio
    private float _SpawnYPositionRatio;
    [SerializeField]
    private float _SpawnZPosition;

    private Vector2 _Res;

    struct MonsterIndexRange
    {
        public int _IndexMin;
        public int _IndexMax;
    }

    [SerializeField]
    List<MonsterIndexRange> _LevelRangeList = new List<MonsterIndexRange>();

    private int _MaxLevel;
    private int _CurrentLevel;

    [SerializeField]
    List<Object> _MonsterPrefabList = new List<Object>();

    void Awake()
    {
        _MangerInstance = this;
        _MaxLevel = _LevelRangeList.Count - 1;
        _Res = new Vector2(Screen.width, Screen.height);
    }

    void Start()
    {
        _CurrentLevel = 0;
    }

    public void LevelUp()
    {
        _CurrentLevel = Mathf.Clamp((_CurrentLevel + 1), 0, _MaxLevel);
    }

    public void SpawnMonster()
    {
        float SpawnXPosition = Random.Range(_SpawnXPositionRatioRng.x, _SpawnXPositionRatioRng.y);
        SpawnXPosition *= _Res.x;
        float SpawnYPosition = _SpawnYPositionRatio * _Res.y;

        int SpawnLevel = Random.Range(0, _CurrentLevel);
        MonsterIndexRange IndexRange = _LevelRangeList[SpawnLevel];
        int SpawnMonsterIndex = Random.Range(IndexRange._IndexMin, IndexRange._IndexMax);

        //@TODO: Spawn Monster From Prefab List
        GameObject SpawnedMonster = Instantiate(_MonsterPrefabList[SpawnMonsterIndex]) as GameObject;
        if(SpawnedMonster != null)
        {
            SpawnedMonster.transform.position = new Vector3(SpawnXPosition, SpawnYPosition, _SpawnZPosition);
        }
        else if(Debug.isDebugBuild)
        {
            Debug.Log("Error on Monster Spawn");
        }
    }
}
