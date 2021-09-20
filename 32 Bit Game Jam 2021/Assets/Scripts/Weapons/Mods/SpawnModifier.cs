using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnModifier : WeaponModifier
{
	[SerializeField] private GameObject Object;

	public override void OnHit(Vector3 _position)
	{
		Instantiate(Object, _position, Quaternion.identity);
	}
}
