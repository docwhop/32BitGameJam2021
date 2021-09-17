using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
	public AudioClip FireAudio;

	public float FireRate;

	public float Speed;

	public float Range;

	public int Damage;

	public ProjectileModifier[] Modifiers;
}
