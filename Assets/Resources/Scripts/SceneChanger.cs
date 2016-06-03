using UnityEngine;
using System.Collections;

namespace ParkJunHo
{
    public class SceneChanger : MonoBehaviour
    {
        public float Second = 0f;
        public string _Scene = "";

        // Use this for initialization
        void Start()
        {
            SceneComponent.Instance.ScheduleLoadScene(Second, _Scene);
        }
    }
}
