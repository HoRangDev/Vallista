using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ParkJunHo
{
    public class BackText : MonoBehaviour
    {
        private Image image = null;
        private float speed = 0.02f;

        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
        }

        public void ShowText()
        {
            Color cor = image.color;
            cor.a = 1;
            image.color = cor;

            StartCoroutine(TextShowed());
        }

        private IEnumerator TextShowed()
        {
            yield return new WaitForSeconds(0.7f);

            while(true)
            {
                Color cor = image.color;
                cor.a -= speed * 62.5f * Time.smoothDeltaTime;
                image.color = cor;

                if(cor.a <= 0)
                {
                    break;
                }

                yield return null;
            }
        }
    }

}
