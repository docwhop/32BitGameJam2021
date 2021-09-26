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
    [SerializeField]
    Animator armsParent;

    WeaponName currentWeapon;

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

		//SwapWeaponModel();
	}

	private void OnEnable()
    {
        EventManager.weaponChangedEvent += SwapWeaponModel;
    }
    private void OnDisable()
    {
        EventManager.weaponChangedEvent -= SwapWeaponModel;
    }
    
    public void SwapWeaponModel(Weapon weapon)
    {
        Debug.Log("SwapWeaponModel called.");
        if(currentWeapon != weaponHandler.GetPrimary().WeaponName)
        {
            switch (weaponHandler.GetPrimary().WeaponName)
            {
                case WeaponName.NeedleGun:
                    currentWeapon = WeaponName.NeedleGun;
                    StartCoroutine(ReloadNeedler());
                    break;
                case WeaponName.HoneyLauncher:
                    currentWeapon = WeaponName.HoneyLauncher;
                    StartCoroutine(ReloadHoneyLauncher());
                    break;
                case WeaponName.Pollenator:
                    currentWeapon = WeaponName.Pollenator;
                    StartCoroutine(ReloadPollenator());
                    break;
                default:
                    break;
            }       
        }
    }

    IEnumerator ReloadNeedler()
    {
        Debug.Log("SwitchNeedler called.");
        armsParent.SetTrigger("SwitchWeapon");
        yield return new WaitForSeconds(.5f);
        needleGun.SetActive(true);
        honeyLauncher.SetActive(false);
        pollenator.SetActive(false);
        needleGun.transform.parent = rightArm.transform;
        needleGun.transform.localRotation = Quaternion.Euler(0, 90, 0);
        needleGun.transform.localPosition = Vector3.zero;
    
    }
    IEnumerator ReloadHoneyLauncher()
    {
        Debug.Log("SwitchHoneyLancher called.");
        armsParent.SetTrigger("SwitchWeapon");
        yield return new WaitForSeconds(.5f);
        honeyLauncher.SetActive(true);
        needleGun.SetActive(false);
        pollenator.SetActive(false);
        honeyLauncher.transform.parent = rightArm.transform;
        honeyLauncher.transform.localRotation = Quaternion.Euler(0, 90, 0);
        honeyLauncher.transform.localPosition = Vector3.zero;
    
    }
    IEnumerator ReloadPollenator()
    {
        Debug.Log("SwitchPollenator called.");
        armsParent.SetTrigger("SwitchWeapon");
        yield return new WaitForSeconds(.5f);
        pollenator.SetActive(true);
        needleGun.SetActive(false);
        honeyLauncher.SetActive(false);
        pollenator.transform.parent = rightArm.transform;
        pollenator.transform.localRotation = Quaternion.Euler(0, 90, 0);
        pollenator.transform.localPosition = Vector3.zero;
      
    }

}
