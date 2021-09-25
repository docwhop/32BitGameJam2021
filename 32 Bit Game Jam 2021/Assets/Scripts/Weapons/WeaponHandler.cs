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

    [SerializeField]
    private AudioSource reload;

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
        reload.Play();
	}

	public bool FirePrimary(Vector3 _weaponEnd, Vector3 _direction, Collider _ignore = null)
	{
		return FireSelected(selectedIndex, _weaponEnd, _direction, _ignore);
	}

	public bool FireSelected(int _index, Vector3 _weaponEnd, Vector3 _direction, Collider _ignore = null)
	{
		if (fireTimer >= Weapons[_index].FireRate)
		{
			if (Weapons[_index].GetType() == typeof(ProjectileWeapon))
			{
				for (int i = 0; i < Weapons[_index].ProjectileCount; i++)
				{
					Vector3 accuracyRng = new Vector3(Random.Range(-Weapons[_index].Accuracy, Weapons[_index].Accuracy), Random.Range(-Weapons[_index].Accuracy, Weapons[_index].Accuracy), Random.Range(-Weapons[_index].Accuracy, Weapons[_index].Accuracy));
					accuracyRng *= 0.01f;

					ProjectileManager.Instance.SpawnProjectile
					(
						_weaponEnd,
						_direction + accuracyRng,
						ParseProjectile(Weapons[_index]).Speed,
						Weapons[_index].Range,
						Weapons[_index].Damage,
						Weapons[_index].WeaponName,
						_ignore
					);
				}
			}
			else if (Weapons[_index].GetType() == typeof(RaycastWeapon))
			{
				for (int i = 0; i < Weapons[_index].ProjectileCount; i++)
				{
					Vector3 accuracyRng = new Vector3(Random.Range(-Weapons[_index].Accuracy, Weapons[_index].Accuracy), Random.Range(-Weapons[_index].Accuracy, Weapons[_index].Accuracy), Random.Range(-Weapons[_index].Accuracy, Weapons[_index].Accuracy));
					accuracyRng *= 0.01f;

					if (Physics.Raycast(_weaponEnd, _direction + accuracyRng, out RaycastHit hitInfo, Weapons[_index].Range))
					{
						if (hitInfo.collider.TryGetComponent(out Health hitHealth) == true)
						{
							hitHealth.Damage(Weapons[_index].Damage);
						}
					}
				}
			}
			else if (Weapons[_index].GetType() == typeof(MeleeWeapon))
			{
				Collider[] hitCols = Physics.OverlapBox(_weaponEnd, ParseMelee(Weapons[_index]).Size, Quaternion.identity);

				for (int i = 0; i < hitCols.Length; i++)
				{
					if (hitCols[i].gameObject != gameObject && hitCols[i].TryGetComponent(out Health hitHealth) == true)
					{
						hitHealth.Damage(Weapons[_index].Damage);
					}
				}
			}

			AudioManager.Instance.RandomizePitchAndVolume(audioSource);
			audioSource.PlayOneShot(Weapons[_index].FireAudio);

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
        EventManager.Instance.WeaponChanged(Weapons[selectedIndex].WeaponName);
    }

	public void NextWeapon()
	{
		selectedIndex++;

		if(selectedIndex >= Weapons.Length)
		{
			selectedIndex = 0;
		}

		fireTimer = 0;
        EventManager.Instance.WeaponChanged(Weapons[selectedIndex].WeaponName);
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
        EventManager.Instance.WeaponChanged(Weapons[selectedIndex].WeaponName);
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
