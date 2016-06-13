using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShoppingItems : MonoBehaviour
{
    [SerializeField]
    private int _Index = 0;
    [SerializeField]
    private Sprite[] _Sprites = new Sprite[3];

	void Start ()
    {
        Refresh();
	}

    public void OnClick()
    {
        switch(ShopComponent.Instance.Items[_Index])
        {
            case 0:
                ShopComponent.Instance.BuyItem(_Index);
                break;

            case 1:
                ShopComponent.Instance.EquipItem(_Index);
                break;
        }

        ShopComponent.Instance.UpdateShopItems();
    }

    public void Refresh()
    {
        Image img = null;
            var ButtonComp = GetComponent<Button>();
            img = GetComponent<Image>();
            switch (ShopComponent.Instance.Items[_Index])
            {
                case 0:
                    img.sprite = _Sprites[0];
                    img.SetNativeSize();
                    ButtonComp.enabled = true;
                    break;
                case 1:
                    img.sprite = _Sprites[1];
                    img.SetNativeSize();
                    ButtonComp.enabled = true;
                    break;
                case 2:
                    img.sprite = _Sprites[2];
                    img.SetNativeSize();
                    ButtonComp.enabled = false;
                    break;
            }
        }
}
