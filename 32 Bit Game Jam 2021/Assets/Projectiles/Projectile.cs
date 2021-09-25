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
	}

	public virtual void Update()
	{
		TTL += Time.deltaTime;

		if(TTL >= Range)
		{
			TTL = 0;
			gameObject.SetActive(false);
		}

		transform.position = transform.position + (Direction * Speed * Time.deltaTime);
		transform.forward = Direction;
	}

	public void IgnoreCollider(Collider col)
	{
		Physics.IgnoreCollision(GetComponent<Collider>(), col, true);
	}

	public virtual void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.GetComponent<Health>())
		{
			collision.gameObject.GetComponent<Health>().Damage(Damage);
		}

		gameObject.SetActive(false);
	}
}
