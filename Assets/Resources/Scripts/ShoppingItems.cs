using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShoppingItems : MonoBehaviour
{
    [SerializeField]
    private int _Index = 0;
    [SerializeField]
    private Sprite[] _Sprites = new Sprite[3];

	// Use this for initialization
	void Start ()
    {
        Image img = null;
        GetComponent<Button>().enabled = true;
        Debug.Log("Index " + _Index + " Item " + ShopComponent.Instance.Items[_Index]);
        switch (ShopComponent.Instance.Items[_Index])
        {
            case 0:
                img = GetComponent<Image>();
                img.sprite = _Sprites[0];
                img.SetNativeSize();
                
                break;
            case 1:
                img = GetComponent<Image>();
                img.sprite = _Sprites[1];
                img.SetNativeSize();
                break;
            case 2:
                img = GetComponent<Image>();
                img.sprite = _Sprites[2];
                img.SetNativeSize();

                GetComponent<Button>().enabled = false;
                break;
        }
	}

    public void UpdateSprite()
    {
        Start();
    }

    public void OnClick()
    {
        ShopComponent.Instance.BuyItem(_Index);
    }
}
