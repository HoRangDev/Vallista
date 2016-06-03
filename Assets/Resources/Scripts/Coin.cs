using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    private int _Amount;
    public int Amount { get { return _Amount; } set { _Amount = value; } }

    void OnTriggerEnter2D(Collider2D Other)
    {
        switch(Other.tag)
        {
            case "Projectile":
                GameManager.Instance.AddCoin(_Amount);
                Collected();
                break;
        }
    }

    void Collected()
    {

    }
}
