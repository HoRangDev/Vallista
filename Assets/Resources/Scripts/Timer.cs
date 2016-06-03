using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    private static Timer _TimerInstance;

    private float _DeltaTimeTolerance = 0.034f;

    private float _ElasedTime;

    private float _LastFrame;
    private float _DeltaTime;
    private float _CurrentFrame;

    public static Timer Instance { get { return _TimerInstance; } }
    public float ElasedTime { get { return _ElasedTime; } }
    public float DeltaTime { get { return _DeltaTime; } }

    void Awake()
    {
        _TimerInstance = this;
    }

    void Update()
    {
        _CurrentFrame = Time.realtimeSinceStartup;
        _DeltaTime = _CurrentFrame - _LastFrame;
        _DeltaTime *= Time.timeScale;
        _LastFrame = _CurrentFrame;
        _ElasedTime += _DeltaTime;
    }

    void LateUpdate()
    {
        _DeltaTime = (Time.deltaTime + Time.smoothDeltaTime + _DeltaTime) * 0.33333f;
        if(_DeltaTime >= _DeltaTimeTolerance)
        {
            _DeltaTime = Time.smoothDeltaTime;
        }
    }
}