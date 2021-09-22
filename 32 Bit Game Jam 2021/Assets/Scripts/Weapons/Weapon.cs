using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum WeaponName
{
    NeedleGun,
    HoneyLauncher,
    Pollenator,
    None,
}

public class Weapon : ScriptableObject
{
    public WeaponName weaponName;    

	public AudioClip FireAudio;

	public float FireRate;

	public float Range;

	public int Damage;


}
