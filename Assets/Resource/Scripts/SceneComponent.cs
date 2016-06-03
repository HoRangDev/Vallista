using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace ParkJunHo
{
    public class SceneComponent : MonoBehaviour
    {
        static SceneComponent instance = null;

        public static SceneComponent GetInstance()
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SceneComponent>();
            }

            if(instance == null)
            {
                instance = new GameObject("SceneComponent", new System.Type[] { typeof(SceneComponent) }).GetComponent<SceneComponent>();
            }

            return instance;
        }

        public void ScheduleLoadScene(float sec, int level)
        {
            StartCoroutine(CSceneChange(sec, level));
        }


        private IEnumerator CSceneChange(float sec, int level)
        {
            yield return new WaitForSeconds(sec);

            SceneManager.LoadScene(level);
        }
    }
}
