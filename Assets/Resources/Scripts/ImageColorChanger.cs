using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageColorChanger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer TargetImage = null;

    [SerializeField]
    private SHSVColor BeginColor;

    [SerializeField]
    private SHSVColor TargetColor;

    [SerializeField]
    private bool bIsLoop;

    [SerializeField]
    private bool bIsLoopWithReverse;

    [SerializeField]
    private bool bIsReverse;

    [SerializeField]
    private float Time;

    private float InvTime;

    private float ElasedTime;

    void Awake()
    {
        if(Time <= 0.0f)
        {
            Time = 1.0f;
        }

        InvTime = (1.0f / Time);
        ElasedTime = 0.0f;
    }

    void Start()
    {
        StartCoroutine("CompUpdate");
    }

    IEnumerator CompUpdate()
    {
        while(true)
        {
            if (TargetImage != null)
            {
                ElasedTime += UnityEngine.Time.smoothDeltaTime;

                bool bIsEndTime = (ElasedTime >= Time);
                bool bIsOnLoop = (bIsLoop || bIsLoopWithReverse);

                if (bIsEndTime)
                {
                    if (!bIsOnLoop)
                    {
                        StopCoroutine("CompUpdate");
                    }
                    else
                    {
                        if (bIsLoopWithReverse)
                        {
                            bIsReverse = !bIsReverse;
                        }

                        ElasedTime = 0.0f;
                    }
                }

                float Percentage = (ElasedTime * InvTime);
                if (bIsReverse)
                {
                    Percentage = (1.0f - Percentage);
                }

                TargetImage.color = SHSVColor.HSVColorToRGB(SHSVColor.Lerp(BeginColor, TargetColor, Percentage));
            }

            yield return null;
        }
    }
}