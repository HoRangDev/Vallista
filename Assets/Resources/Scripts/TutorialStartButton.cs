using UnityEngine;
using System.Collections;

namespace ParkJunHo
{
    public class TutorialStartButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneChanger changer = new GameObject("Changer", new System.Type[] { typeof(SceneChanger) }).GetComponent<SceneChanger>();
            changer._Scene = "GameScene";
        }
    }
}
