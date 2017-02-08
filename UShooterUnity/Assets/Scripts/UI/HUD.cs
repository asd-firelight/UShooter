using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour 
{
    PlayerController Player = null;
    public List<UiWeaponInfo> WeaponLabels = new List<UiWeaponInfo>();

    public UnityEngine.UI.Text HealthLabel;

    public UnityEngine.UI.Text AmmoLabel = null;
    public UnityEngine.UI.Text AmmoTypeLabel = null;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Player != null)
        {
            Weapon[] weaps = Player.GetWeapons();

            int index = 0;
            foreach (var weapon in weaps)
            {
                if (weapon.HasIt && index < WeaponLabels.Count)
                {
                    if (index < WeaponLabels.Count)
                    {
                        WeaponLabels[index].ShowInfo(weapon.WeaponName, Player.GetAmmo(weapon), Player.ActiveWeapon == weapon);
                        index++;
                    }
                }
            }

            HealthLabel.text = Player.IntHealth().ToString();
            AmmoLabel.text = Player.GetAmmo().ToString();
        }
    }    

    /*
    GUI.color = Color.white;
    GUI.Box(new Rect(Screen.width / 2.0f - 150, 16, 300, 60), "--- HA-HA, YOU ARE DEAD! ---\nDon't worry, be happy!\n- Press Enter to restart -");

    if (Input.GetKeyDown(KeyCode.Return))
        Application.LoadLevel(Application.loadedLevel);
    */

}
