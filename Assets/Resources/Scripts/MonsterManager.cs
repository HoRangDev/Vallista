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

    private Vector2 _Res;

    [System.Serializable]
    struct MonsterIndexRange
    {
        [SerializeField]
        private int _IndexMin;
        public int IndexMin { get { return _IndexMin; } }

        [SerializeField]
        private int _IndexMax;
        public int IndexMax { get { return _IndexMax; } }

        [SerializeField]
        private MonsterSpawnRatio _SpawnRatio;
        public MonsterSpawnRatio SpawnRatio { get { return _SpawnRatio; } }
    }

    [System.Serializable]
    struct MonsterSpawnRatio
    {
        [SerializeField]
        private float[] _Ratios;
        public float GetRatio(int Index)
        {
            if(Index >= 0 && Index < _Ratios.Length)
            {
                return _Ratios[Index];
            }

            return 0.0f;
        }
    }

    [SerializeField]
    private List<MonsterIndexRange> _LevelRangeList = new List<MonsterIndexRange>();

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

            int SpawnLevel = 0;
            MonsterSpawnRatio SpawnRatio = _LevelRangeList[Mathf.Min(_CurrentLevel, _LevelRangeList.Count - 1)].SpawnRatio;
            float Ratio = Random.Range(0.0f, 1.0f);
            for (int TargetLevel = 0; TargetLevel < _LevelRangeList.Count; ++TargetLevel) 
            {
                if(Ratio <= SpawnRatio.GetRatio(TargetLevel))
                {
                    SpawnLevel = TargetLevel;
                    break;
                }
            }

            MonsterIndexRange IndexRange = _LevelRangeList[SpawnLevel];
            int SpawnMonsterIndex = Random.Range(IndexRange.IndexMin, IndexRange.IndexMax + 1);

            GameObject SpawnedMonster = Instantiate(_MonsterPrefabList[SpawnMonsterIndex]) as GameObject;
            if (SpawnedMonster != null)
            {
                SpawnedMonster.transform.position = new Vector3(SpawnXPosition, SpawnYPosition, 0.0f);
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
