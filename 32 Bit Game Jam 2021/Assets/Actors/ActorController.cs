using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : ScriptableObject
{
	[HideInInspector] public Transform Player;

	protected Actor AttachedActor;

	protected Animator Animator;

	public virtual void Initialize(Actor _attachedActor)
	{
		AttachedActor = _attachedActor;

		Animator = AttachedActor.GetComponent<Animator>();

		Player = Camera.main.transform.parent;
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

	public bool CanSeePlayer()
	{
		if(Vector3.Distance(AttachedActor.transform.position, Player.position) <= 40)
		{
			if(Physics.Raycast(AttachedActor.transform.position, PlayerDirection(), out RaycastHit raycastHit, 40) == true)
			{
				if(raycastHit.transform.tag == "Player")
				{
					return true;
				}
			}
		}

		return false;
	}
}
