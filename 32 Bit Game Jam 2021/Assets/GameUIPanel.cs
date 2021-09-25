using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIPanel : MonoBehaviour
{

    [SerializeField]
    Sprite beeBuster, honeyLauncher, pollenator;
    [SerializeField]
    Image weaponImage;
    [SerializeField]
    TextMeshProUGUI ammoText;
    private void OnEnable()
    {
        EventManager.weaponFiredEvent += UpdateUI;
        EventManager.weaponChangedEvent += SwitchWeaponIcon;
    }

    private void OnDisable()
    {

        EventManager.weaponFiredEvent -= UpdateUI;
        EventManager.weaponChangedEvent -= SwitchWeaponIcon;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        
        
    }

    public void SwitchWeaponIcon(WeaponName name)
    {
        if(name == WeaponName.NeedleGun)
        {
            weaponImage.sprite = beeBuster;
        }
        else if(name == WeaponName.HoneyLauncher)
        {
            weaponImage.sprite = honeyLauncher;
        }
        else if(name == WeaponName.Pollenator)
        {
            weaponImage.sprite = pollenator;
        }


    }
}
