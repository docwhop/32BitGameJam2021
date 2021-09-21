using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Weapon : ScriptableObject
{
	public AudioClip FireAudio;

	public float FireRate;

	public float Range;

	public int Damage;
}
