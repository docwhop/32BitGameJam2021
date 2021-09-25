using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMechController", menuName = "Actor controllers/Mech controller")]
public class MechController : ActorController
{
	Vector3 direction;

	bool lightAttacking;

	public override void Initialize(Actor _attachedActor)
	{
		base.Initialize(_attachedActor);

		direction = AttachedActor.transform.forward;

		lightAttacking = false;
	}

	public override void FixedUpdate()
	{
		if (CanSeePlayer() == true)
		{
			if(Animator.GetCurrentAnimatorStateInfo(0).IsName("MoveTurn") == true || Animator.GetCurrentAnimatorStateInfo(0).IsName("MeleeAttack") == true)
			{
				direction = Vector3.MoveTowards(AttachedActor.transform.forward, PlayerDirection(), 2 * Time.fixedDeltaTime).normalized;
				direction.y = 0;

				AttachedActor.transform.forward = direction;
			}

			if (Vector3.Distance(Player.position, AttachedActor.transform.position) <= 20)
			{
				Animator.SetTrigger("MeleeAttack");
			}
			else
			{
				float angle = Vector3.SignedAngle(PlayerDirection(), direction, Vector3.up);

				if (angle <= 25 && angle >= -25)
				{
					//Front
					if (Vector3.Distance(Player.position, AttachedActor.transform.position) <= 50)
					{
						Animator.SetTrigger("LightAttack");
					}
					else
					{
						Animator.SetTrigger("HeavyAttack");
					}
				}
				else
				{
					//Back
					Animator.SetInteger("MoveState", 3);
				}
			}
		}

		if(lightAttacking == true)
		{
			AttachedActor.WeaponHandler.FireSelected(1, AttachedActor.GunEnds[1].position, -AttachedActor.GunEnds[1].forward, AttachedActor.Collider);
		}

		//Debug.Log(Vector3.Distance(Player.position, AttachedActor.transform.position) + " : " + CanSeePlayer());
	}

	public override void AttackEvent()
	{
		AttachedActor.WeaponHandler.FireSelected(0, AttachedActor.GunEnds[0].position, Vector3.zero, AttachedActor.Collider);
	}

	public override void LightAttackEvent()
	{
		lightAttacking = !lightAttacking;
	}

	public override void HeavyAttackEvent()
	{
		AttachedActor.WeaponHandler.FireSelected(2, AttachedActor.GunEnds[2].position, -AttachedActor.GunEnds[2].right, AttachedActor.Collider);
	}
}
