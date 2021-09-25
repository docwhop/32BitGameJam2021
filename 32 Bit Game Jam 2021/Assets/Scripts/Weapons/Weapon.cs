using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum WeaponName
{
    NeedleGun,
    HoneyLauncher,
    Pollenator,
	Cannon,
    None,
}

public class Weapon : ScriptableObject
{
    public WeaponName WeaponName;

	public AudioClip FireAudio;

	public float FireRate;

	public float Accuracy;

	public int ProjectileCount;

	public float Range;

	public int Damage;
}
