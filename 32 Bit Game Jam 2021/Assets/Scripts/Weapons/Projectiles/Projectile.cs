using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	Vector3 direction;

	float speed;

	float range; //Seconds

	int damage;

	WeaponModifier[] modifiers;

	float ttl;

	public void Initialize(Vector3 _position, Vector3 _direction, float _speed, float _range, int _damage, WeaponModifier[] _modifiers)
	{
		transform.position = _position;
		direction = _direction;
		speed = _speed;
		range = _range;
		damage = _damage;

		modifiers = _modifiers;

		for (int i = 0; i < modifiers.Length; i++)
		{
			modifiers[i].Initialize();
		}

		transform.forward = direction;

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

		for (int i = 0; i < modifiers.Length; i++)
		{
			modifiers[i].Update();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.GetComponent<Health>())
		{
			collision.gameObject.GetComponent<Health>().Damage(damage);
		}

		for (int i = 0; i < modifiers.Length; i++)
		{
			modifiers[i].OnHit(transform.position);
		}

		gameObject.SetActive(false);
	}
}
