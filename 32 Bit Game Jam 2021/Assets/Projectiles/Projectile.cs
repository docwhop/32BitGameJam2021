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
	public float ExplosionMin;
	public float ExplosionMax;

	Vector3 previousPosition;

	Collider col;

	Collider ignored;

	public virtual void Awake()
	{
		col = GetComponent<Collider>();	
	}

	public virtual void Initialize(Vector3 _position, Vector3 _direction, float _speed, float _range, int _damage)
	{
		transform.position = _position;
		Direction = _direction;
		Speed = _speed;
		Range = _range;
		Damage = _damage;

		transform.forward = Direction;

		TTL = 0;

		previousPosition = transform.position;

		if(ignored != null && col != null)
		{
			Physics.IgnoreCollision(col, ignored, false);
			ignored = null;
		}
	}

	public virtual void Update()
	{
		TTL += Time.deltaTime;

		if(TTL >= Range)
		{
			if(Explodes == true)
			{
				Explosion();
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
				Explosion();
			}

			Debug.Log(hit.transform.name);

			gameObject.SetActive(false);
		}
	}

	public void IgnoreCollider(Collider _col)
	{
		Physics.IgnoreCollision(col, _col, true);
		ignored = _col;
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.GetComponent<Health>())
		{
			collision.gameObject.GetComponent<Health>().Damage(Damage);
		}

		if (Explodes == true)
		{
			Explosion();
		}

		gameObject.SetActive(false);
	}

	public virtual void Explosion()
	{
		ExplosionManager.Instance.SpawnExplosion(transform.position, Damage, 10, ExplosionMin, ExplosionMax);
	}
}
