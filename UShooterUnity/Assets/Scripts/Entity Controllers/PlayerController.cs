using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController: Creature 
{
    public Weapon ActiveWeapon = null;

    Animator CharAnimator = null;
    HUD Hud = null;

    float MovementSpeed = 4.2f;
    float Gravity = 40.0f;
    float JumpForce = 18.0f;

    bool Jumping = false;

    Vector3 PrevPos;
    Vector3 JumpVector = Vector3.zero;
    float JumpTimer = 0;
    bool JumpTerminated = false;

    int NumFireFrames = 0;

    Weapon[] Weapons = null;

    int[] Ammo = null;

    public bool HasWeapon(WeaponType Type)
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i].Type == Type)
            {
                if (Weapons[i].HasIt)
                    return true;                
            }
        }
        return false;
    }

    public void AddWeapon(WeaponType Type)
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i].Type == Type)
            {
                if (!Weapons[i].HasIt)
                {
                    Weapons[i].HasIt = true;
                    ActiveWeapon = Weapons[i];
                }
                Ammo[(int)ActiveWeapon.Ammo] += ActiveWeapon.DefaultAmmoCount;                
            }
        }
    }

    public override void OnStart() 
    {
        CharAnimator = GetComponentInChildren<Animator>();

        Weapons = gameObject.GetComponentsInChildren<Weapon>();

        Ammo = new int[(int)AmmoType.NumAmmoTypes];
        for (int i = 0; i < (int)AmmoType.NumAmmoTypes; i++) Ammo[i] = 0;

        Hud = GameObject.FindObjectOfType(typeof(HUD)) as HUD;
        PrevPos = transform.position;

        AddWeapon(WeaponType.RocketLauncher);
        AddWeapon(WeaponType.RailGun);
	}

    public void AddAmmo(AmmoType type, int count)
    {
        Ammo[(int)type] += count;
    }

    void Fire()
    {
        int AmmoCount = ActiveWeapon.MakeShot(this, Ammo[(int)ActiveWeapon.Ammo]);

        if (AmmoCount != Ammo[(int)ActiveWeapon.Ammo])
        {
            Ammo[(int)ActiveWeapon.Ammo] = AmmoCount;

            CharAnimator.SetBool("Fire", true);
            NumFireFrames = 1;
        }
    }

    public override void Death()
    {
        base.Death();

        CharAnimator.SetBool("IsDead", true);
    }

    public void SetWeapon(WeaponType Type)
    {
        if (ActiveWeapon.Type == Type || !HasWeapon(Type)) return;

        for (int i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i].Type == Type)
                ActiveWeapon = Weapons[i];
        }
    }

	void Update () 
    {
        if (NumFireFrames > 0)
        {
            NumFireFrames++;

            if(NumFireFrames > 5)
            {
                NumFireFrames = 0;
                CharAnimator.SetBool("Fire", false);
            }
        }        

        Vector3 vec = Vector3.zero;

        if (!IsDead())
        {
            vec = UpdateInputAndMovement();
        }
        UpdateMovement(vec);
    }

    Vector3 UpdateInputAndMovement()
    {
        Vector3 vec = Vector3.zero;

        /// Weapon firing
        /// ////////////////////////////////////////////////////////////////////////////////
        if (Controls.Weapon1())
        {
            SetWeapon(WeaponType.RocketLauncher);
        }

        if (Controls.Weapon2())
        {
            SetWeapon(WeaponType.RailGun);
        }

        if (Controls.Fire() && ActiveWeapon.WillShoot(Ammo[(int)ActiveWeapon.Ammo]))
        {
            Fire();
        }

        /// Movement
        /// ////////////////////////////////////////////////////////////////////////////////
        bool MovingNow = false;

        if (Controls.MoveLeft())
        {
            transform.LookAt(transform.position - Camera.main.transform.right);

            vec = transform.forward;
            MovingNow = true;
        }

        if (Controls.MoveRight())
        {
            transform.LookAt(transform.position + Camera.main.transform.right);

            vec = transform.forward;
            MovingNow = true;
        }        

        if (Controls.Jump() && JumpTimer < 0)
        {
            if (DownDistance() < 0.2f)
            {
                JumpTimer = 0.3f;
                Jumping = true;
                JumpTerminated = false;

                JumpVector = new Vector3(0, Gravity + JumpForce, 0);
            }
        }

        CharAnimator.SetFloat("Speed", MovingNow ? 1.0f : 0.0f);
        return vec;
    }

    void UpdateMovement(Vector3 vec)
    {
        float Speed = MovementSpeed * Time.deltaTime;

        if (Jumping)
        {
            JumpVector.y = JumpVector.y - Gravity * Time.deltaTime;
            if (JumpVector.y <= 0)
            {
                Jumping = false;
                JumpVector = new Vector3(0, 0, 0);
            }
        }
        CharAnimator.SetBool("Jump", Jumping);

        JumpTimer -= Time.deltaTime;
        Char.Move(vec * Speed + new Vector3(0, -Gravity * Time.deltaTime, 0) + JumpVector * Time.deltaTime);

        Vector3 ActualMove = transform.position - PrevPos;
        Camera.main.transform.position += ActualMove;

        if (Jumping && Mathf.Abs(ActualMove.y) < 0.001f && !JumpTerminated)
        {
            JumpVector = JumpVector * 0.5f;
            JumpTerminated = true;
        }
        CharAnimator.SetBool("JumpFinished", JumpTerminated);

        PrevPos = transform.position;
    }

    /** Returns player weapons */
    public Weapon[] GetWeapons()
    {
        return Weapons;
    }

    /** Returns ammo of current weapon */
    public int GetAmmo()
    {
        if(ActiveWeapon == null)
        {
            return 0;
        }

        return Ammo[(int)ActiveWeapon.Ammo];
    }

    /** Returns ammo of specified type */
    public int GetAmmo(AmmoType Type)
    {
        return Ammo[(int)Type];
    }

    /** Returns ammo for specified weapon */
    public int GetAmmo(Weapon w)
    {
        return Ammo[(int)w.Ammo];
    }
}
