using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
    [SerializeField]
    private float _Speed;

    private float _Angle = 0.0f;

    void Awake()
    {
        StartCoroutine("Rotate");
    }

    IEnumerator Rotate()
    {
        while(true)
        {
            _Angle += (_Speed * Timer.Instance.DeltaTime);
            if(_Angle <= -360.0f || _Angle >= 360.0f)
            {
                _Angle = 0.0f;
            }

            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, _Angle);
            yield return null;
        }
    }
}
