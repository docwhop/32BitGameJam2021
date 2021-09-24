using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	Vector3 direction;

	float speed;

	float range; //Seconds

	int damage;

	float ttl;

	public void Initialize(Vector3 _position, Vector3 _direction, float _speed, float _range, int _damage)
	{
		transform.position = _position;
		direction = _direction;
		speed = _speed;
		range = _range;
		damage = _damage;

		transform.forward = direction;

		//Bad but resets IgnoreCollider values
		GetComponent<Collider>().enabled = false;
		GetComponent<Collider>().enabled = true;

		ttl = 0;
	}

	void Update()
	{
		ttl += Time.deltaTime;

		if(ttl >= range)
		{
			ttl = 0;
			gameObject.SetActive(false);
		}

		transform.position = transform.position + (direction * speed * Time.deltaTime);
		transform.forward = direction;
	}

	public void IgnoreCollider(Collider col)
	{
		Physics.IgnoreCollision(GetComponent<Collider>(), col, true);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.GetComponent<Health>())
		{
			collision.gameObject.GetComponent<Health>().Damage(damage);
		}

		gameObject.SetActive(false);
	}
}
