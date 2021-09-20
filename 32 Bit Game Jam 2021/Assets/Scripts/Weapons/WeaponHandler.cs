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
			switch(GetPrimary().Type)
			{
				case WeaponType.Projectile:
					ProjectileManager.Instance.SpawnProjectile
					(
						_weaponEnd,
						_direction,
						GetPrimary().Speed,
						GetPrimary().Range,
						GetPrimary().Damage,
						GetPrimary().Modifiers
					);
					break;
				case WeaponType.Raycast:
					if(Physics.Raycast(_weaponEnd, _direction, out RaycastHit hitInfo, GetPrimary().Range))
					{
						if(hitInfo.collider.TryGetComponent(out Health hitHealth) == true)
						{
							hitHealth.Damage(GetPrimary().Damage);

							for (int i = 0; i < GetPrimary().Modifiers.Length; i++)
							{
								GetPrimary().Modifiers[i].OnHit(hitInfo.point);
							}
						}
					}
					break;
				case WeaponType.Melee: //Melee needs testing (could setup wireframe gizmo)
					Collider[] hitCols = Physics.OverlapBox(_weaponEnd, (Vector3.one * 2) + transform.forward, Quaternion.identity);

					for (int i = 0; i < hitCols.Length; i++)
					{
						if (hitCols[i].TryGetComponent(out Health hitHealth) == true)
						{
							hitHealth.Damage(GetPrimary().Damage);

							for (int m = 0; m < GetPrimary().Modifiers.Length; m++)
							{
								GetPrimary().Modifiers[i].OnHit(hitCols[i].transform.position);
							}
						}
					}
					break;
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
}
