using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AmmoType
{
    Rockets=0,
    Charges,

    NumAmmoTypes,
};

public enum WeaponType
{
    RocketLauncher = 0,
    RailGun,

    NumWeapons,
};

public class Weapon : MonoBehaviour 
{
    public AmmoType Ammo = AmmoType.Rockets;
    public WeaponType Type = WeaponType.RocketLauncher;

    public int DefaultAmmoCount = 10;
    public int NumAmmoForShot = 1;

    public bool HasIt = false;
    public string WeaponName = "Rocket Launcher";

    public Color LightColor = Color.yellow;

    public virtual int MakeShot(Creature Shooter, int Ammo)
    {
        return Ammo;
    }

    public virtual bool WillShoot(int Ammo)
    {
        return false;
    }

    public virtual void OnUpdate()
    {
    }

	// Use this for initialization
	void Start () 
    {
        OnStart();
	}

    public virtual void OnStart()
    {
    }
	
	// Update is called once per frame
	void Update () 
    {
        OnUpdate();	
	}
}
