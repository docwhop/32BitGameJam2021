using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : ScriptableObject
{
	[HideInInspector] public Collider Player;

	protected Actor AttachedActor;

	protected Animator Animator;

	public float SightRange;

	public virtual void Initialize(Actor _attachedActor)
	{
        Debug.Log("Initialzing actor");
		AttachedActor = _attachedActor;

		Animator = AttachedActor.GetComponent<Animator>();

		Player = Camera.main.transform.parent.GetComponent<Collider>();
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

	public virtual void AttackEvent()
	{

	}

	public virtual void LightAttackEvent()
	{

	}

	public virtual void HeavyAttackEvent()
	{

	}

    public virtual void DeathEvent()
    {

    }

	public Vector3 GetDirection(Vector3 position)
	{
		return (position - AttachedActor.transform.position).normalized;
	}

	public Vector3 PlayerPosition()
	{
		return Player.bounds.center;
	}

	public Vector3 PlayerDirection()
	{
		return GetDirection(PlayerPosition());
	}

	public bool CanSeePlayer()
	{
		if(Vector3.Distance(AttachedActor.transform.position, PlayerPosition()) <= SightRange)
		{
			return true;
		}

		return false;
	}
}
