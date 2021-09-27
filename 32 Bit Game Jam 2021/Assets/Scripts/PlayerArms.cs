using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    Animator animator;

    private void OnEnable()
    {
        EventManager.weaponReloadedEvent += ReloadWeapon;
        EventManager.weaponFiredEvent += FireWeapon;
    }

    private void OnDisable()
    {
        EventManager.weaponReloadedEvent -= ReloadWeapon;
        EventManager.weaponFiredEvent += FireWeapon;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void ReloadWeapon(Weapon weapon)
    {

        switch (weapon.WeaponName)
        {
            case WeaponName.NeedleGun:
                animator.SetTrigger("ReloadBeeBuster");
                break;
            case WeaponName.HoneyLauncher:
                animator.SetTrigger("ReloadHoneyLauncher");
                break;
            case WeaponName.Pollenator:
                animator.SetTrigger("ReloadPollenizer");
                break;
            default:
                break;
        }
       
    }

    public void FireWeapon()
    {
        animator.SetTrigger("Recoil");
    }
}
