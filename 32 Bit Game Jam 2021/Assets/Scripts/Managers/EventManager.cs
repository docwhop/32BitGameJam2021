using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void OnKeyPickupDelegate();
    public static event OnKeyPickupDelegate keyPickupEvent;

    public delegate void OnWeaponChangedDelegate(Weapon weapon);
    public static event OnWeaponChangedDelegate weaponChangedEvent;

    public delegate void OnWeaponReloadDelegate();
    public static event OnWeaponReloadDelegate weaponReloadedEvent;

    public delegate void OnWeaponFiredDelegate();
    public static event OnWeaponFiredDelegate weaponFiredEvent;

    public delegate void OnPlayerDiedDelegate();
    public static event OnPlayerDiedDelegate playerDiedEvent;



    private static EventManager _instance;

    public static EventManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    public void KeyPickedUp()
    {
        keyPickupEvent?.Invoke();
    }

    public void WeaponChanged(Weapon weapon)
    {
        //Debug.Log("Weapon Changed called in EventManager");
        weaponChangedEvent?.Invoke(weapon);
    }

    public void WeaponReloaded()
    {
        weaponReloadedEvent?.Invoke();
    }

    public void WeaponFired()
    {
        weaponFiredEvent?.Invoke();
    }

    public void PlayerDied()
    {
        playerDiedEvent?.Invoke();

    }

}
