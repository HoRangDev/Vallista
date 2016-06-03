using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Mesh _Mesh;
    private Transform _Transform;
    private Renderer _Renderer;

    void Awake()
    {
        _Transform = transform;
        _Renderer = GetComponent(typeof(Renderer)) as Renderer;
    }
}
