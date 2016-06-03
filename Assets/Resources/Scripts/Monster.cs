using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private int _Health;

    [SerializeField]
    private float _Speed;

    void OnTriggetEnter2D(Collider2D Other)
    {
        switch(Other.tag)
        {
            case "Projectile":
                Projectile TargetProjectile = Other.GetComponent(typeof(Projectile)) as Projectile;
                if(TargetProjectile != null)
                {
                    _Health -= TargetProjectile.Damage;
                    TargetProjectile.Destroy();

                    if(_Health <= 0)
                    {
                        Dead();
                    }
                    else
                    {
                        Attacked();
                    }
                }
                break;
        }
    }

    void Attacked()
    {
        //@TODO: Add Attacked Animation
    }

    void Dead()
    {
        //@TODO: Add Dead Animation and Destroy Process

    }
}
