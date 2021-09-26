using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	protected Vector3 Direction;

	protected float Speed;

	protected float Range; //Seconds

	protected int Damage;

	protected float TTL;

	public bool Explodes;

	Vector3 previousPosition;

	public virtual void Initialize(Vector3 _position, Vector3 _direction, float _speed, float _range, int _damage)
	{
		transform.position = _position;
		Direction = _direction;
		Speed = _speed;
		Range = _range;
		Damage = _damage;

		transform.forward = Direction;

		//Bad but resets IgnoreCollider values
		GetComponent<Collider>().enabled = false;
		GetComponent<Collider>().enabled = true;

		TTL = 0;

		previousPosition = transform.position;
	}

	public virtual void Update()
	{
		TTL += Time.deltaTime;

		if(TTL >= Range)
		{
			if(Explodes == true)
			{
				ExplosionManager.Instance.SpawnExplosion(transform.position, 1, 10, 3, 5);
			}

			TTL = 0;

			gameObject.SetActive(false);
		}

		transform.position = transform.position + (Direction * Speed * Time.deltaTime);
		transform.forward = Direction;

		if(Speed > 100)
		{
			PreciseCollision();
		}

		previousPosition = transform.position;
	}

	void PreciseCollision()
	{
		//Bit shift to make a layer mask that excludes projectiles
		int projectileMask = 1 << 7;
		projectileMask = ~projectileMask;

		if (Physics.Raycast(transform.position, (previousPosition - transform.position).normalized, out RaycastHit hit, Vector3.Distance(previousPosition, transform.position), projectileMask))
		{
			if (hit.transform.GetComponent<Health>())
			{
				hit.transform.GetComponent<Health>().Damage(Damage);
			}

			if (Explodes == true)
			{
				ExplosionManager.Instance.SpawnExplosion(transform.position, 1, 10, 3, 5);
			}

			Debug.Log(hit.transform.name);

			gameObject.SetActive(false);
		}
	}

	public void IgnoreCollider(Collider col)
	{
		Physics.IgnoreCollision(GetComponent<Collider>(), col, true);
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Health>())
		{
			other.gameObject.GetComponent<Health>().Damage(Damage);
		}

		if (Explodes == true)
		{
			ExplosionManager.Instance.SpawnExplosion(transform.position, 1, 10, 3, 5);
		}

		gameObject.SetActive(false);
	}
}
