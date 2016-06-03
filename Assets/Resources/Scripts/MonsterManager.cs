using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    private static MonsterManager _MangerInstance;
    public static MonsterManager Instance { get { return _MangerInstance; } }

    // Ratio -0.5 ~ 0.5
    [SerializeField]
    private Vector2 _SpawnXPositionRatioRng;
    [SerializeField]
    // Ratio
    private Vector2 _SpawnYPositionRatioRng;
    [SerializeField]
    private float _SpawnZPosition;

    private Vector2 _Res;

    [System.Serializable]
    struct MonsterIndexRange
    {
        public int _IndexMin;
        public int _IndexMax;
    }

    [SerializeField]
    List<MonsterIndexRange> _LevelRangeList = new List<MonsterIndexRange>();

    private int _MaxLevel;
    private int _CurrentLevel;
    public int CurrentLevel { get { return _CurrentLevel; } }

    [SerializeField]
    List<Object> _MonsterPrefabList = new List<Object>();

    [SerializeField]
    private int _MaxSpawnNum;

    [SerializeField]
    private float _MaxMonsterSpawnTime;

    void Awake()
    {
        _MangerInstance = this;
        _MaxLevel = _LevelRangeList.Count - 1;
        _Res = new Vector2(720.0f, 1280.0f);
    }

    void Start()
    {
        _CurrentLevel = 0;
        SpawnMonster();
    }

    public void LevelUp()
    {
        _CurrentLevel = Mathf.Clamp((_CurrentLevel + 1), 0, _MaxLevel);
    }

    public void SpawnMonster()
    {
        for (int Index = 0; Index < Random.Range(1, _MaxSpawnNum); ++Index)
        {
            float SpawnXPosition = Random.Range(_SpawnXPositionRatioRng.x, _SpawnXPositionRatioRng.y);
            SpawnXPosition *= _Res.x;
            float SpawnYPosition = Random.Range(_SpawnYPositionRatioRng.x, _SpawnYPositionRatioRng.y);
            SpawnYPosition *= _Res.y;

            int SpawnLevel = Random.Range(0, _CurrentLevel);
            MonsterIndexRange IndexRange = _LevelRangeList[SpawnLevel];
            int SpawnMonsterIndex = Random.Range(IndexRange._IndexMin, IndexRange._IndexMax);

            GameObject SpawnedMonster = Instantiate(_MonsterPrefabList[SpawnMonsterIndex]) as GameObject;
            if (SpawnedMonster != null)
            {
                SpawnedMonster.transform.position = new Vector3(SpawnXPosition, SpawnYPosition, _SpawnZPosition);
                SpawnedMonster.SendMessage("SetToMove");
            }
            else if (Debug.isDebugBuild)
            {
                Debug.Log("Error on Monster Spawn");
            }
        }

        Invoke("SpawnMonster", Random.Range(2.5f, _MaxMonsterSpawnTime));
    }
}
