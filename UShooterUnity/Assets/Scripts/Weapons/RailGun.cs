using UnityEngine;
using System.Collections;

public class RailGun : Weapon
{
    float ShotLight = 0;
    float ShotLightTime = 0.1f;
    public ParticleEmitter ShotParticles = null;
    public Light ShotFlashLight = null;

    float ShotWait = 0;

    public GameObject ShotgunHit = null;
    public float Damage = 100;

    public Transform BulletPoint = null;

    public LineRenderer Trail = null;
   

    public override void OnStart()
    {
        WeaponName = "RAILGUN";

        Ammo = AmmoType.Charges;
        Trail.enabled = false;
    }

    public override void OnUpdate()
    {
        ShotWait -= Time.deltaTime;

        if (ShotLight > 0)
        {
            ShotLight -= Time.deltaTime;

            if (ShotFlashLight != null)
            {
                ShotFlashLight.color = LightColor;
            }
            float L = ShotLight / ShotLightTime;

            if (ShotLight <= 0)
            {
                if (ShotFlashLight != null)
                    ShotFlashLight.enabled = !true;

                Trail.enabled = false;
            }
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

        if (ShotParticles != null)
        {
            ShotParticles.Emit();
        }

        if (ShotFlashLight != null)
        {
            ShotFlashLight.enabled = true;
        }
        Trail.enabled = true;

        ShotLight = ShotLightTime;
        ShotWait = 0.6f;

        Vector3 point = BulletPoint.transform.position;
        Vector3 vec = Shooter.transform.forward;

        float Dmg = Damage;
        {
            Vector3 projVec = vec;
            float DistanceToWall = 100.0f;

            RaycastHit rh = new RaycastHit();

            Trail.transform.position = BulletPoint.transform.position;
            if (Physics.Raycast(point, projVec, out rh, DistanceToWall, 1 << LayerMask.NameToLayer("Ground")))
            {
                DistanceToWall = rh.distance;

                if (ShotgunHit != null)
                    GameObject.Instantiate(ShotgunHit, rh.point + rh.normal * 0.3f, rh.transform.rotation);

                Trail.SetPosition(1, new Vector3(0, 0, rh.distance));
            }
            else
                Trail.SetPosition(1, new Vector3(0, 0, DistanceToWall));

            var hits = Physics.RaycastAll(point, projVec, DistanceToWall, Shooter.DamageMask);
            foreach(var hit in hits)
            {
                GameObject obj = hit.collider.gameObject;

                obj.SendMessage("OnReceivedDamage", Dmg, SendMessageOptions.DontRequireReceiver);

                Creature target = obj.GetComponent<Creature>();
                if (target)
                    target.ReceiveDamage(Dmg, Shooter);
            }
        }

        return Ammo - NumAmmoForShot;

    }
}
