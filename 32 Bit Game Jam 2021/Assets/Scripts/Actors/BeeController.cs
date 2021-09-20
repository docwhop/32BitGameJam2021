using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BeeController : ActorController
{
	Vector3 target;

	Vector3 direction;

	public override void Initialize(Actor _attachedActor)
	{
		base.Initialize(_attachedActor);

		NewTarget();
	}

	public override void FixedUpdate()
	{
		if (AttachedActor.WeaponHandler.FirePrimary(AttachedActor.transform.position, PlayerDirection()) == true)
		{
			//Shot projectile
		}

		if (Vector3.Distance(target, AttachedActor.transform.position) <= 1)
		{
			NewTarget();
		}

		if (direction != Vector3.zero) //Stops unnecessary movement
		{
			AttachedActor.Rbody.MovePosition(AttachedActor.transform.position + (direction * AttachedActor.Data.Acceleration));
		}

		if (AttachedActor.Rbody.velocity.magnitude > AttachedActor.Data.MaxSpeed) //Stops velocity going crazy
		{
			AttachedActor.Rbody.velocity = AttachedActor.Rbody.velocity.normalized * AttachedActor.Data.MaxSpeed;
		}
	}

	void NewTarget()
	{
		target = Player.position;

		int yMin = 10;
		int yMax = 15;

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

		direction = (target - AttachedActor.transform.position).normalized;
	}

	public override void OnCollisionEnter(Collision collision)
	{
		//Stops flying enemy from getting glitched on the ground or walls
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Default"))
		{
			NewTarget(); //Not great
		}
	}
}
