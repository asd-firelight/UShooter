using UnityEngine;
using System.Collections;

public class Controls
{
    public static bool MoveLeft()
    {
        return Input.GetButton("MoveLeft");
    }

    public static bool MoveRight()
    {
        return Input.GetButton("MoveRight");
    }

    public static bool Fire()
    {
        return Input.GetButton("Fire");
    }

    public static bool Jump()
    {
        return Input.GetButton("Jump");
    }

    public static bool Weapon1()
    {
        return Input.GetButtonDown("Weapon1");
    }

    public static bool Weapon2()
    {
        return Input.GetButtonDown("Weapon2");
    }
}
