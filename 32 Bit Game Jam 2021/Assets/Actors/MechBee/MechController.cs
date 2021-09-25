using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMechController", menuName = "Actor controllers/Mech controller")]
public class MechController : ActorController
{
	Vector3 direction;

	public override void Initialize(Actor _attachedActor)
	{
		base.Initialize(_attachedActor);

		direction = AttachedActor.transform.forward;
	}

	public override void FixedUpdate()
	{
		if(CanSeePlayer() == true)
		{
			if(Vector3.Distance(Player.position, AttachedActor.transform.position) <= 15)
			{
				Animator.SetTrigger("MeleeAttack");

				direction = Vector3.MoveTowards(AttachedActor.transform.forward, PlayerDirection(), 1.5f * Time.fixedDeltaTime).normalized;
				direction.y = 0;

				AttachedActor.transform.forward = direction;
			}
			else
			{
				float angle = Vector3.SignedAngle(PlayerDirection(), direction, Vector3.up);

				if (angle <= 30 && angle >= -30)
				{
					//Front

					//Attack or move forward/back
				}
				else
				{
					//Back
					Animator.SetInteger("MoveState", 3);

					direction = Vector3.MoveTowards(AttachedActor.transform.forward, PlayerDirection(), 2 * Time.fixedDeltaTime).normalized;
					direction.y = 0;

					AttachedActor.transform.forward = direction;
				}
			}
		}
	}
}
