using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WeaponHandler : MonoBehaviour
{
	public Weapon[] Weapons;

	int selectedIndex;

	float fireTimer;

	AudioSource audioSource;

	void Awake()
	{
		audioSource = GetComponent<AudioSource>();	
	}

	void Update()
	{
		fireTimer += Time.deltaTime;
	}

    public void Reload()
    {
		EventManager.Instance.WeaponReloaded();
	}

	public bool FirePrimary(Vector3 _weaponEnd, Vector3 _direction, Collider _ignore = null)
	{
		if(fireTimer >= GetPrimary().FireRate)
		{
			if(GetPrimary().GetType() == typeof(ProjectileWeapon))
			{
				for (int i = 0; i < GetPrimary().ProjectileCount; i++)
				{
					Vector3 accuracyRng = new Vector3(Random.Range(-GetPrimary().Accuracy, GetPrimary().Accuracy), Random.Range(-GetPrimary().Accuracy, GetPrimary().Accuracy), Random.Range(-GetPrimary().Accuracy, GetPrimary().Accuracy));
					accuracyRng *= 0.01f;
					
					ProjectileManager.Instance.SpawnProjectile
					(
						_weaponEnd,
						_direction + accuracyRng,
						ParseProjectile(GetPrimary()).Speed,
						GetPrimary().Range,
						GetPrimary().Damage, 
                        GetPrimary().WeaponName,
						_ignore
					);
				}
			}
			else if(GetPrimary().GetType() == typeof(RaycastWeapon))
			{
				for (int i = 0; i < GetPrimary().ProjectileCount; i++)
				{
					Vector3 accuracyRng = new Vector3(Random.Range(-GetPrimary().Accuracy, GetPrimary().Accuracy), Random.Range(-GetPrimary().Accuracy, GetPrimary().Accuracy), Random.Range(-GetPrimary().Accuracy, GetPrimary().Accuracy));
					accuracyRng *= 0.01f;

					if (Physics.Raycast(_weaponEnd, _direction + accuracyRng, out RaycastHit hitInfo, GetPrimary().Range))
					{
						if (hitInfo.collider.TryGetComponent(out Health hitHealth) == true)
						{
							hitHealth.Damage(GetPrimary().Damage);
						}
					}
				}
			}
			else if(GetPrimary().GetType() == typeof(MeleeWeapon))
			{
				Collider[] hitCols = Physics.OverlapBox(_weaponEnd, ParseMelee(GetPrimary()).Size, Quaternion.identity);

				for (int i = 0; i < hitCols.Length; i++)
				{
					if (hitCols[i].gameObject != gameObject && hitCols[i].TryGetComponent(out Health hitHealth) == true)
					{
						hitHealth.Damage(GetPrimary().Damage);
					}
				}
			}

            AudioManager.Instance.RandomizePitchAndVolume(audioSource);
            audioSource.PlayOneShot(GetPrimary().FireAudio);

			fireTimer = 0;

			return true;
		}

		return false;
	}

	public Weapon GetPrimary()
	{
		return Weapons[selectedIndex];
	}

	public void SelectWeapon(int _index)
	{
		selectedIndex = _index;

		if(selectedIndex >= Weapons.Length)
		{
			selectedIndex = Weapons.Length - 1;
		}

		fireTimer = 0;
        EventManager.Instance.WeaponChanged();
    }

	public void NextWeapon()
	{
		selectedIndex++;

		if(selectedIndex >= Weapons.Length)
		{
			selectedIndex = 0;
		}

		fireTimer = 0;
        EventManager.Instance.WeaponChanged();
    }

    public void PreviousWeapon()
    {
        if(selectedIndex == 0)
        {
            selectedIndex = Weapons.Length - 1;  
        }
        else
        {
            selectedIndex--;
        }

        fireTimer = 0;
        EventManager.Instance.WeaponChanged();
    }

	ProjectileWeapon ParseProjectile(Weapon _weapon)
	{
		return (ProjectileWeapon)_weapon;
	}

	RaycastWeapon ParseRaycast(Weapon _weapon)
	{
		return (RaycastWeapon)_weapon;
	}

	MeleeWeapon ParseMelee(Weapon _weapon)
	{
		return (MeleeWeapon)_weapon;
	}
}
