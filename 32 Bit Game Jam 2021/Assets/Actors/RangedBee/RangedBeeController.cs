using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRangedBeeController", menuName = "Actor controllers/Ranged bee controller")]
public class RangedBeeController : ActorController
{
	Vector3 target;

	Vector3 direction;

	float newTargetTimer;

	float attackTimer;

	int gunEndIndex;

	bool attacking;

	public override void Initialize(Actor _attachedActor)
	{
		base.Initialize(_attachedActor);

		NewTarget();

		newTargetTimer = 0;
		attacking = false;
		attackTimer = 0;
	}

	public override void FixedUpdate()
	{
        Debug.Log("Checking can see");
		if(CanSeePlayer() == true)
		{
            Debug.Log("Can See player: ");
			newTargetTimer += Time.deltaTime;

			attackTimer += Time.deltaTime;

			if (attacking == true)
			{
				Vector3 gunDir = (PlayerPosition() - AttachedActor.GunEnds[gunEndIndex].position).normalized;

				if (AttachedActor.WeaponHandler.FirePrimary(AttachedActor.GunEnds[gunEndIndex].position, gunDir, AttachedActor.Collider) == true)
				{
					gunEndIndex++;

					if (gunEndIndex >= AttachedActor.GunEnds.Length)
					{
						gunEndIndex = 0;
					}
				}
			}
			else
			{
				if (attackTimer >= 3)
				{
					Animator.SetTrigger("Attack");
					attackTimer = 0;
				}
			}

			if (Vector3.Distance(target, AttachedActor.transform.position) <= 1 || newTargetTimer >= 2)
			{
				NewTarget();

				newTargetTimer = 0;
			}

			direction = Vector3.Lerp(direction, (target - AttachedActor.transform.position).normalized, 1 * Time.deltaTime);

			if (direction != Vector3.zero) //Stops unnecessary movement
			{
				AttachedActor.Rbody.MovePosition(AttachedActor.transform.position + (direction * AttachedActor.Data.Acceleration));
			}

			AttachedActor.transform.LookAt(PlayerPosition());

			if (AttachedActor.Rbody.velocity.magnitude > AttachedActor.Data.MaxSpeed) //Stops velocity going crazy
			{
				AttachedActor.Rbody.velocity = AttachedActor.Rbody.velocity.normalized * AttachedActor.Data.MaxSpeed;
			}

			float angle = Vector3.SignedAngle(PlayerDirection(), direction, Vector3.up);

			if (angle <= 45 && angle >= -45)
			{
				//Front
				Animator.SetInteger("MoveState", 1);
			}
			else if (angle <= -45 && angle >= -135)
			{
				//Right
				Animator.SetInteger("MoveState", 3);
			}
			else if (angle >= 45 && angle <= 135)
			{
				//Left
				Animator.SetInteger("MoveState", 4);
			}
			else
			{
				//Back
				Animator.SetInteger("MoveState", 2);
			}
		}
		else
		{
			Animator.SetInteger("MoveState", 0);
		}
	}

	public override void AttackEvent()
	{
		attacking = !attacking;
		attackTimer = 0;
	}

	public override void DeathEvent()
	{
		if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("take_death"))
		{
			// not the best. shifts the collider to align with character death so they don't fall through the floor.
			Animator.SetTrigger("Died");

			AttachedActor.gameObject.layer = LayerMask.NameToLayer("DeadEnemy");

			AttachedActor.Rbody.velocity = Vector3.zero;
			AttachedActor.Rbody.angularVelocity = Vector3.zero;
			AttachedActor.Rbody.freezeRotation = true;
			AttachedActor.Rbody.AddForce(Vector3.down * 10, ForceMode.Impulse);

			CapsuleCollider collider = AttachedActor.GetComponent<CapsuleCollider>();
			collider.center = new Vector3(collider.center.x, 1.86f, collider.center.z);

			//Improves performance
			AttachedActor.enabled = false;
		}
	}

	void NewTarget()
	{
		target = PlayerPosition();

		int yMin = 10;
		int yMax = 15;

		target += Vector3.up * Random.Range(yMin, yMax);

		int xMin = 15;
		int xMax = 25;

		if (Random.Range(0, 2) == 0)
		{
			target += Vector3.right * Random.Range(xMin, xMax);
		}
		else
		{
			target += Vector3.left * Random.Range(xMin, xMax);
		}

		if (Random.Range(0, 2) == 0)
		{
			target += Vector3.forward * Random.Range(xMin, xMax);
		}
		else
		{
			target += Vector3.back * Random.Range(xMin, xMax);
		}
	}

	public override void OnCollisionEnter(Collision collision)
	{
		//Stops flying enemy from getting glitched on the ground or walls
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Default"))
		{
			NewTarget(); //Not great

			newTargetTimer = 0;
		}
	}
}
