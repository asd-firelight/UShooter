using UnityEngine;
using System.Collections;

public class RocketLauncher : Weapon 
{
    public GameObject RocketPrefab = null;
    public GameObject RocketSpawnTransform = null;

    float ShotLight = 0;
    float ShotLightTime = 0.1f;
    public ParticleEmitter ShotParticles = null;
    public Light ShotFlashLight = null;

    float ShotWait = 0;

    public GameObject ShotgunHit = null;
    public float ShotgunDamage = 20;

    public Transform BulletPoint = null;

    public override void OnStart()
    {

    }

    float RandomSign()
    {
        float r = Random.Range(-1.0f, 1.0f);

        if (r > 0) return 1.0f;
        return -1.0f;
    }

    public override void OnUpdate()
    {
        ShotWait -= Time.deltaTime;

        if (ShotLight > 0 && ShotFlashLight)
        {
            ShotLight -= Time.deltaTime;

            float L = ShotLight / ShotLightTime;

            if (ShotLight <= 0)
                ShotFlashLight.enabled = !true;
        }
    }

    public override bool WillShoot(int Ammo)
    {
        if (Ammo < NumAmmoForShot || ShotWait > 0)
            return false;

        return true;
    }

    public override int MakeShot(Creature Shooter, int Ammo)
    {
        if (!WillShoot(Ammo)) 
            return Ammo;

        GameObject Rocket = GameObject.Instantiate(RocketPrefab, RocketSpawnTransform.transform.position, transform.root.rotation) as GameObject;
        Rocket.GetComponent<RocketProjectile>().Owner = GetComponentInParent<PlayerController>();

        if (ShotParticles != null)
        {
            ShotParticles.Emit();
        }

        if (ShotFlashLight != null)
        {
            ShotFlashLight.enabled = true;
            ShotLight = ShotLightTime;
        }

        ShotWait = 0.5f;
        return Ammo - NumAmmoForShot;
    }
}
