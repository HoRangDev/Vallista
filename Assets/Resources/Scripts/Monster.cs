using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Monster : MonoBehaviour
{
    [SerializeField]
    private float _ShockDelay;

    [SerializeField]
    private int _Health;

    [SerializeField]
    private float _Speed;

    public void SetToMove()
    {
        //@TODO: Add Walk Animation
        StartCoroutine("Movement");
    }

    IEnumerator Movement()
    {
        while(true)
        {
            Vector2 Position = transform.position;
            Position.y -= (_Speed * Timer.Instance.DeltaTime);
            transform.position = Position;
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.CompareTag("Projectile"))
        {
            Projectile TargetProjectile = Other.GetComponent(typeof(Projectile)) as Projectile;
            if (TargetProjectile != null)
            {
                _Health -= TargetProjectile.Damage;
                TargetProjectile.Destroy();

                if (_Health <= 0)
                {
                    Dead();
                }
                else
                {
                    Attacked();
                }
            }
        }
    }

    void Attacked()
    {
        //@TODO: Add Attacked Animation
        StopCoroutine("Movement");
        Invoke("SetToMove", _ShockDelay);
    }

    void Dead()
    {
        //@TODO: Add Dead Animation and Destroy Process
        //@TODO: Add Random Coin Drop Process
        StopCoroutine("Movement");
    }
}
