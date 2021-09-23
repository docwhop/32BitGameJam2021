using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{
    Animator animator;

    private void OnEnable()
    {
        EventManager.weaponReloadedEvent += ReloadWeapon;
    }

    private void OnDisable()
    {
        EventManager.weaponReloadedEvent -= ReloadWeapon;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void ReloadWeapon()
    {
        animator.SetTrigger("Reload");
    }
}
