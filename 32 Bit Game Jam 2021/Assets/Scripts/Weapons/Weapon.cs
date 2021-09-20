using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	Projectile,
	Raycast,
	Melee
}

public class Weapon : ScriptableObject
{
	public WeaponType Type;

	public AudioClip FireAudio;

	public float FireRate;

	public float Speed;

	public float Range;

	public int Damage;

	public Bounds MeleeBounds;

	public WeaponModifier[] Modifiers;
}
