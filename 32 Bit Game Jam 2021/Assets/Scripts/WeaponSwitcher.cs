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

	void Update()
	{
		SwitchInput();
	}

	void SwitchInput()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			weaponHandler.NextWeapon();
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			weaponHandler.PreviousWeapon();
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			weaponHandler.SelectWeapon(0);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			weaponHandler.SelectWeapon(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			weaponHandler.SelectWeapon(2);
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			weaponHandler.Reload();
		}

		SwapWeaponModel();
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
        //Debug.Log("SwapWeaponModel called.");
        switch (weaponHandler.GetPrimary().WeaponName)
        {
            case WeaponName.NeedleGun:
                //Debug.Log("Needle gun equipped.");
                needleGun.SetActive(true);
                honeyLauncher.SetActive(false);
                pollenator.SetActive(false);
                needleGun.transform.parent = rightArm.transform;
                needleGun.transform.localPosition = Vector3.zero;
                break;
            case WeaponName.HoneyLauncher:
                //Debug.Log("Honey Launcher equipped.");
                honeyLauncher.SetActive(true);
                needleGun.SetActive(false);
                pollenator.SetActive(false);
                honeyLauncher.transform.parent = rightArm.transform;
                honeyLauncher.transform.localPosition = Vector3.zero;

                break;
            case WeaponName.Pollenator:
                //Debug.Log("Pollenator equipped.");
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
