using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager _ManagerInstance;
    public static GameManager Instance { get { return _ManagerInstance; } }

    private int _CurrentScore;
    public int CurrentScore { get { return _CurrentScore; } }

    void Awake()
    {
        _ManagerInstance = this;
    }

}
