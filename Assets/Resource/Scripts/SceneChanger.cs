using UnityEngine;
using System.Collections;

namespace ParkJunHo
{
    public class SceneChanger : MonoBehaviour
    {
        public float Second = 0f;
        public int Level = 0;

        // Use this for initialization
        void Start()
        {
            SceneComponent.Instance.ScheduleLoadScene(Second, Level);
        }
    }
}
