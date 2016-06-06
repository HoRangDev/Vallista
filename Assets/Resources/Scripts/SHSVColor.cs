using UnityEngine;

// Reference:
// https://en.wikipedia.org/wiki/HSL_and_HSV
[System.Serializable]
public struct SHSVColor
{
    public float H;
    public float S;
    public float V;

    public SHSVColor(float _H, float _S, float _V)
    {
        H = _H;
        S = _S;
        V = _V;
        InitClampHSV();
    }

    public SHSVColor(float _R, float _G, float _B, float _A)
    {
        this = RGBToHSVColor(new Color(_R, _G, _B, _A));
        InitClampHSV();
    }

    public SHSVColor(Color RGBColor)
    {
        this = RGBToHSVColor(RGBColor);
        InitClampHSV();
    }

    private void InitClampHSV()
    {
        H = Mathf.Clamp(H, 0.0f, 360.0f);
        S = Mathf.Clamp(S, 0.0f, 1.0f);
        V = Mathf.Clamp(V, 0.0f, 1.0f);
    }

    public static SHSVColor Lerp(SHSVColor From, SHSVColor To, float T)
    {
        float t = Mathf.Clamp01(T);
        float HDelta = To.H - From.H;
        float SDelta = To.S - From.S;
        float VDelta = To.V - From.V;

        float NewH = From.H + (HDelta * t);
        float NewS = From.S + (SDelta * t);
        float NewV = From.V + (VDelta * t);

        return new SHSVColor(NewH, NewS, NewV);
    }

    public static Color HSVColorToRGB(SHSVColor HSVColor)
    {
        Color Result = new Color();

        if(HSVColor.S <= 0.0f)
        {
            Result.r = HSVColor.V;
            Result.g = HSVColor.V;
            Result.b = HSVColor.V;
        }
        else
        {
            float InH, p, q, t, ff;
            long i;
            InH = HSVColor.H;
            if(InH >= 360.0f)
            {
                InH = 0.0f;
            }

            InH /= 60.0f;
            i = (long)InH;
            ff = InH - i;

            p = HSVColor.V * (1.0f - HSVColor.S);
            q = HSVColor.V * (1.0f - (HSVColor.S * ff));
            t = HSVColor.V * (1.0f - (HSVColor.S * (1.0f - ff)));

            switch(i)
            {
                case 0:
                    Result.r = HSVColor.V;
                    Result.g = t;
                    Result.b = p;
                    break;

                case 1:
                    Result.r = q;
                    Result.g = HSVColor.V;
                    Result.b = p;
                    break;

                case 2:
                    Result.r = p;
                    Result.g = HSVColor.V;
                    Result.b = t;
                    break;

                case 3:
                    Result.r = p;
                    Result.g = q;
                    Result.b = HSVColor.V;
                    break;

                case 4:
                    Result.r = t;
                    Result.g = p;
                    Result.b = HSVColor.V;
                    break;

                default:
                    Result.r = HSVColor.V;
                    Result.g = p;
                    Result.b = q;
                    break;
            }
        }

        Result.a = 1.0f;

        return Result;
    }

    public static SHSVColor RGBToHSVColor(Color RGBColor)
    {
        //@TODO: Implement of RGBTOHSVCOLOR
        return new SHSVColor();
    }
}