using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//@TODO: 추후에 일반 텍스트도 가능한 버전으로 변경.
// 텍스트의 사이즈는 스프라이트의 크기에 종속적임.
public class NumberImageText : MonoBehaviour
{
    /// <summary>
    /// 내부 이미지 리스트
    /// </summary>
    private List<SpriteRenderer> _TextImageList = new List<SpriteRenderer>();

    [SerializeField]
    private float _Alpha = 1;

    public float Alpha
    {
        set
        {
            if (_TextImageList.Count > 0)
            {
                float AlphaRate = Mathf.Clamp01(value);
                _Alpha = AlphaRate;
                Color NewColor = _TextImageList[0].color;
                NewColor.a = _Alpha;

                for (int Index = 0; Index < _TextImageList.Count; ++Index)
                {
                    _TextImageList[Index].color = NewColor;
                }
            }
        }
        get
        {
            return _Alpha;
        }
    }

    /// <summary>
    /// 숫자 스프라이트들 (Index Equal Sprites Number)
    /// </summary>
    [SerializeField]
    private Sprite[] _NumberSprites = new Sprite[10];

    /// <summary>
    /// 커스텀 간격을 사용할지 이미지의 크기를 사용할지 결정합니다.
    /// </summary>
    [SerializeField]
    private bool _bIsUseCustomSpace = false;
    public bool IsUseCustomSpace
    {
        get { return _bIsUseCustomSpace; }
    }

    /// <summary>
    /// 커스텀 간격
    /// </summary>
    [SerializeField]
    private float _CustomSpace = 0.0f;

    [SerializeField]
    private float _TextScale = 1.0f;
   
    /// <summary>
    /// 텍스트 이미지의 Pivot
    /// </summary>
    [SerializeField]
    private Vector2 _Pivot;
    public Vector2 Pivot
    {
        get { return _Pivot; }
        set
        {
            _Pivot = value;
            for(int Index = 0; Index < _TextImageList.Count; ++Index)
            {
                //_TextImageList[Index].rectTransform.pivot = _Pivot;
            }
        }
    }

    /// <summary>
    /// 텍스트의 Pivot
    /// </summary>
    [SerializeField]
    private Vector2 _TextPivot;
    public Vector2 TextPivot
    {
        get { return _TextPivot; }
        set
        {
            _TextPivot = value;
        }
    }

    private Vector2 _Size;
    public Vector2 Size
    {
        get { return _Size; }
        set { _Size = value; }
    }

    /// <summary>
    /// 표시할 텍스트 내용
    /// </summary>
    [SerializeField]
    private string _Text;
    public string Text
    {
        get { return _Text; }
        set
        {
            _Text = value;
            Refresh();
        }
    }

    void Awake()
    {
        Refresh();
    }

    void Update()
    {
        Refresh();
    }

    /// <summary>
    /// 현재 설정된 정보를 기반으로 텍스트를 새로고침합니다.
    /// </summary>
    void Refresh()
    {
        /// 이미지가 새로 추가된만큼만 새로 생성함.
        if(Text.Length > _TextImageList.Count)
        {
            int LengthGap = (Text.Length - _TextImageList.Count);
            for(int Index = 0; Index < LengthGap; ++Index)
            {
                //@TODO: 오브젝트 풀 사용하기
                GameObject ObjectTmp = new GameObject("TextChlid", typeof(SpriteRenderer));
                ObjectTmp.transform.SetParent(transform);
                SpriteRenderer ImageTmp = ObjectTmp.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                ImageTmp.sortingOrder = 1;
                _TextImageList.Add(ImageTmp);
            }
        }

        // TextImageList's Length equal Text's Length
        Vector2 Size = Vector2.zero;
        /// 스프라이트 설정 설정
        for (int Index = 0;Index < _TextImageList.Count; ++Index)
        {
            int TextToInt = int.Parse(Text[Index].ToString());

            bool bIsDuplicated = _TextImageList[Index].sprite == _NumberSprites[TextToInt];
            if(!bIsDuplicated)
            {

                _TextImageList[Index].sprite = _NumberSprites[TextToInt];
                _TextImageList[Index].transform.localScale = new Vector3(_TextScale, _TextScale, 1.0f);
            }

            if (!_bIsUseCustomSpace)
            {
                float ImageWidth = _TextImageList[Index].sprite.rect.width;
                Size.x += ImageWidth;
            }

            Size.y = Mathf.Max(Size.y, _TextImageList[Index].sprite.rect.height);
        }

        if(_bIsUseCustomSpace)
        {
            Size.x = (_CustomSpace * Text.Length);
            Size.y *= _TextScale;
        }
        else
        {
            Size *= _TextScale;
        }

        this.Size = Size;
        Pivot = Pivot;

        /// 텍스트 위치 설정
        Vector2 BeginPosition = new Vector2((this.Size.x * (TextPivot.x - 1)), (this.Size.y * (0.5f - TextPivot.y)));
        Vector2 Position = Vector2.zero;
        for (int Index = 0; Index < _TextImageList.Count; ++Index)
        {
            float ImageWidth = _TextImageList[Index].sprite.rect.width;

            if (IsUseCustomSpace)
            {
                Position.x = BeginPosition.x + (_CustomSpace * Index) + (_CustomSpace * Pivot.x);
            }
            else
            {
                Position.x = BeginPosition.x + ((ImageWidth) * (Index)) ;
            }

            Position.y = BeginPosition.y;
            _TextImageList[Index].transform.localPosition = new Vector3(Position.x, Position.y, 10);
        }

        /// 텍스트 Alpha 값 설정
        Alpha = Alpha;
    }
}
