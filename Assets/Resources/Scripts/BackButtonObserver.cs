using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ParkJunHo
{
    public class BackButtonObserver : MonoBehaviour
    {
        private bool mBackPressed = false;
        private BackText mBackText = null;

        // Use this for initialization
        void Start()
        {
            mBackText = FindObjectOfType<BackText>();
            DontDestroyOnLoad(this);
        }

        void OnLevelWasLoaded(int level)
        {
            mBackText = FindObjectOfType<BackText>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (mBackPressed)
                {
                    Debug.Log("Quit Game");
                    Application.Quit();
                }
                else
                {
                    mBackPressed = true;
                    //스프라이트 띄우기
                    mBackText.ShowText();

                    StartCoroutine(RelieveBackButton());
                }
            }
        }

        private IEnumerator RelieveBackButton()
        {
            yield return new WaitForSeconds(0.7f);

            mBackPressed = false;
        }
    }
}
