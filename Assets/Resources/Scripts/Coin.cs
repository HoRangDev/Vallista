using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Other)
    {
        switch(Other.tag)
        {
            case "Projectile":
                GameManager.Instance.AddCoin(1);
                Collected();
                break;
        }
    }

    void Collected()
    {

    }
}
