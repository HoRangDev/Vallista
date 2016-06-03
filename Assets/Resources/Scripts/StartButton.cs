using UnityEngine;
using System.Collections;

namespace ParkJunHo
{
    public class StartButton : MonoBehaviour
    {
        private TitleSceneChangeEffect effect = null;

        void Start()
        {
            effect = FindObjectOfType<TitleSceneChangeEffect>();
        }

        public void OnClick()
        {
            effect.StartEffect();
        }
    }
}