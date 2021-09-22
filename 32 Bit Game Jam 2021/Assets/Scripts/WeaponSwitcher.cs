using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField]
    Transform leftArm, rightArm;
    [SerializeField]
    GameObject needleGun, honeyLauncher, pollenator;
    [SerializeField]
    WeaponHandler weaponHandler;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        EventManager.weaponChangedEvent += SwapWeaponModel;
    }
    private void OnDisable()
    {
        EventManager.weaponChangedEvent -= SwapWeaponModel;
    }
    
    public void SwapWeaponModel()
    {
        switch (weaponHandler.GetPrimary().weaponName)
        {
            case WeaponName.NeedleGun:
            
                needleGun.SetActive(true);
                honeyLauncher.SetActive(false);
                pollenator.SetActive(false);
                needleGun.transform.parent = rightArm.transform;
                needleGun.transform.localPosition = Vector3.zero;
                break;
            case WeaponName.HoneyLauncher:
                honeyLauncher.SetActive(true);
                needleGun.SetActive(false);
                pollenator.SetActive(false);
                honeyLauncher.transform.parent = rightArm.transform;
                honeyLauncher.transform.localPosition = Vector3.zero;

                break;
            case WeaponName.Pollenator:
                pollenator.SetActive(true);
                needleGun.SetActive(false);
                honeyLauncher.SetActive(false);
                pollenator.transform.parent = rightArm.transform;
                pollenator.transform.localPosition = Vector3.zero;
                break;
            default:
                break;
        }
       
    }
}
