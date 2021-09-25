using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeBeeController", menuName = "Actor controllers/Melee bee controller")]
public class MeleeBeeController : ActorController
{
	Vector3 target;

	Vector3 direction;

	public float AttackRange;
	public float ChargeRange;

	float timer;

	public override void Initialize(Actor _attachedActor)
	{
		base.Initialize(_attachedActor);

		NewTarget();
	}

	public override void FixedUpdate()
	{
		if(CanSeePlayer() == true && !AttachedActor.IsDead)
		{
			timer += Time.deltaTime;

			if (Vector3.Distance(AttachedActor.transform.position, Player.transform.position) <= AttackRange) //Attack
			{
				Animator.SetTrigger("Attack");

				direction = Vector3.zero;
			}
			else if (Vector3.Distance(AttachedActor.transform.position, Player.transform.position) <= ChargeRange) //Charge
			{
				direction = PlayerDirection();
			}
			else //Hover
			{
				if (Vector3.Distance(target, AttachedActor.transform.position) <= 1 || timer >= 2)
				{
					NewTarget();

					timer = 0;
				}
			}

			direction = Vector3.Lerp(direction, (target - AttachedActor.transform.position).normalized, 1 * Time.deltaTime);

			if (direction != Vector3.zero) //Stops unnecessary movement
			{
				AttachedActor.Rbody.MovePosition(AttachedActor.transform.position + (direction * AttachedActor.Data.Acceleration));
			}

			AttachedActor.transform.LookAt(Player);

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
		AttachedActor.WeaponHandler.FirePrimary(AttachedActor.transform.position, PlayerDirection());
	}

    public override void DeathEvent()
    {
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("take_death"))
        {
            // not the best. shifts the collider to align with character death so they don't fall through the floor.
            Animator.SetTrigger("Died");
            AttachedActor.GetComponent<Rigidbody>().useGravity = true;
            CapsuleCollider collider = AttachedActor.GetComponent<CapsuleCollider>();
            collider.center = new Vector3(collider.center.x, 1.86f, collider.center.z); 
        }
    }

	void NewTarget()
	{
		target = Player.position;

		int yMin = 0;
		int yMax = 10;

		target += Vector3.up * Random.Range(yMin, yMax);

		int xMin = 10;
		int xMax = 20;

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
            if (AttachedActor.IsDead)
            {
                AttachedActor.GetComponent<Rigidbody>().useGravity = false;
            }
			NewTarget(); //Not great

			timer = 0;
		}
	}
}
