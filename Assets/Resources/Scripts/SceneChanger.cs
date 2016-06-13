using UnityEngine;
using System.Collections;

namespace ParkJunHo
{
    public class SceneChanger : MonoBehaviour
    {
        public float Second = 0f;
        public string _Scene = "";
        public bool _bIsStartOnChange = true;

        void Start()
        {
            if (_bIsStartOnChange)
            {
                SceneComponent.Instance.ScheduleLoadScene(Second, _Scene);
            }
        }

        public void Event()
        {
            SceneComponent.Instance.ScheduleLoadScene(Second, _Scene);
        }
    }
}
