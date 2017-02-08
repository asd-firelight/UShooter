using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    public Creature Owner = null;
    public float Damage = 10;
    public GameObject HitEffect = null;

    public float FlySpeed = 10.0f;

    void Update()
    {
        gameObject.transform.position += gameObject.transform.forward * FlySpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag != "Player")
        {
            Creature target = collision.collider.GetComponent<Creature>();

            if (target != Owner)
            {
                if (target)
                    target.ReceiveDamage(Damage, Owner);
                else
                    collision.collider.gameObject.SendMessage("OnReceivedDamage", Damage, SendMessageOptions.DontRequireReceiver);
            }

            if (HitEffect != null)
                GameObject.Instantiate(HitEffect, gameObject.transform.position, gameObject.transform.rotation);

            GameObject.Destroy(gameObject);
        }
    }
}
