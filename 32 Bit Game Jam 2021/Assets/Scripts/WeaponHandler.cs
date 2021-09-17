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
			ProjectileManager.Instance.SpawnProjectile
			(
				_weaponEnd,
				_direction,
				GetPrimary().Speed,
				GetPrimary().Range,
				GetPrimary().Damage,
				GetPrimary().Modifiers
			);

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
