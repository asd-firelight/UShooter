using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int Health = 20;

    public GameObject DestructionEffect = null;

    public void OnReceivedDamage(float Dmg)
    {
        if(Health > 0)
        {
            Health -= (int)Dmg;
            if(Health <=0)
            {
                OnObjectDestroyed();
            }
        }
    }

    public virtual void OnObjectDestroyed()
    {
        if(DestructionEffect != null)
        {
            GameObject.Instantiate(DestructionEffect, gameObject.transform.position, gameObject.transform.rotation);
        }

        Destroy(gameObject);
    }
}
