using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiWeaponInfo : MonoBehaviour
{
    public UnityEngine.UI.Text WeaponNameLabel = null;
    public UnityEngine.UI.Text WeaponAmmoLabel = null;

    public void ShowInfo(string WeapName, int AmmoCount, bool IsActive)
    {
        WeaponNameLabel.text = WeapName;
        WeaponAmmoLabel.text = AmmoCount.ToString();

        Color SelectedColor = Color.white;

        if (!IsActive)
        {
            SelectedColor.a = 0.65f;
        }

        WeaponNameLabel.color = SelectedColor;
        WeaponAmmoLabel.color = SelectedColor;
    }
}
