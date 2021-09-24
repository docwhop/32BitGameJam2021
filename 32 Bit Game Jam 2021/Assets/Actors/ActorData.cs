using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActorData : ScriptableObject
{
	//Makes setting values easier to read, 1 instead of 0.001f
	public float Acceleration { get { return acceleration * 0.01f; } }
	[SerializeField] private float acceleration;

	[SerializeField] private float maxSpeed;
	public float MaxSpeed { get { return maxSpeed * 0.01f; } }
}
