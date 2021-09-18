using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : ScriptableObject
{
	[HideInInspector] public Transform Player;

	protected Actor AttachedActor;

	public virtual void Initialize(Actor _attachedActor)
	{
		AttachedActor = _attachedActor;

		Player = Camera.main.transform; //Really bad
	}

	public virtual void Update()
	{

	}

	public virtual void FixedUpdate()
	{

	}

	public virtual void OnCollisionEnter(Collision collision)
	{

	}

	public Vector3 PlayerDirection()
	{
		return (Player.position - AttachedActor.transform.position).normalized;
	}
}
