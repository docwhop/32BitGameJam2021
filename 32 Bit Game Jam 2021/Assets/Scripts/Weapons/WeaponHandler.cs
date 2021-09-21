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

	public bool FirePrimary(Vector3 _weaponEnd, Vector3 _direction)
	{
		if(fireTimer >= GetPrimary().FireRate)
		{
			if(GetPrimary().GetType() == typeof(ProjectileWeapon))
			{
				ProjectileManager.Instance.SpawnProjectile
				(
					_weaponEnd,
					_direction,
					ParseProjectile(GetPrimary()).Speed,
					GetPrimary().Range,
					GetPrimary().Damage
				);
			}
			else if(GetPrimary().GetType() == typeof(RaycastWeapon))
			{
				if (Physics.Raycast(_weaponEnd, _direction, out RaycastHit hitInfo, GetPrimary().Range))
				{
					if (hitInfo.collider.TryGetComponent(out Health hitHealth) == true)
					{
						hitHealth.Damage(GetPrimary().Damage);
					}
				}
			}
			else if(GetPrimary().GetType() == typeof(MeleeWeapon))
			{
				Collider[] hitCols = Physics.OverlapBox(_weaponEnd + (_direction * GetPrimary().Range), ParseMelee(GetPrimary()).Size, Quaternion.identity);

				for (int i = 0; i < hitCols.Length; i++)
				{
					if (hitCols[i].TryGetComponent(out Health hitHealth) == true)
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
	}

	public void NextWeapon()
	{
		selectedIndex++;

		if(selectedIndex >= Weapons.Length)
		{
			selectedIndex = 0;
		}

		fireTimer = 0;
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
