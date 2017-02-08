using UnityEngine;
using System.Collections;

public class Creature: MonoBehaviour 
{
    [HideInInspector]
    public CharacterController Char = null;

    public float Health = 100.0f;
    public LayerMask DamageMask;

    void Start()
    {
        Char = GetComponentInChildren<CharacterController>();

        OnStart();
    }

    public virtual void OnStart()
    {
    }

    public void ReceiveDamage(float Dmg, Creature From)
    {
        if (Health <= 0) return; // dead already

        Health -= Dmg;

        if (Health <= 0)
        {
            OnDamaged(Dmg, From, true);
            Death();
        }
        else
            OnDamaged(Dmg, From, false);
    }

    public virtual void OnDamaged(float dmg, Creature From, bool Death)
    {
        if (Death && From != null)
            From.OnKilledByMe(this);

    }

    public virtual void OnKilledByMe(Creature victim)
    {
    }

    public bool IsDead()
    {
        return Health <= 0;
    }

    public int IntHealth()
    {
        return (int)Health;
    }

    public virtual void Death()
    {
    }

    public bool InTheAir()
    {
        return DownDistance() > 0.1f;
    }

    public float UpDistance()
    {
        RaycastHit rh = new RaycastHit();
        if (Physics.Raycast(transform.position, new Vector3(0, 1, 0), out rh))
        {
            return rh.distance - Char.height / 2.0f;
        }
        return 100.0f;
    }

    public float DownDistance()
    {
        RaycastHit rh = new RaycastHit();

        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out rh))
        {
            return rh.distance - Char.height / 2.0f;
        }

        return 100.0f;
    }    

    public float Height()
    {
        return Char.height;
    }
}
