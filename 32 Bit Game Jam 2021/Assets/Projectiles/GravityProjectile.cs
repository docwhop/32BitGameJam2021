using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityProjectile : Projectile
{
	Rigidbody rbody;

	public override void Awake()
	{
		base.Awake();

		rbody = GetComponent<Rigidbody>();
	}

	public override void Initialize(Vector3 _position, Vector3 _direction, float _speed, float _range, int _damage)
	{
		base.Initialize(_position, _direction, _speed, _range, _damage);

		rbody.velocity = Vector3.zero;

		rbody.AddForce(Direction * (Speed * 0.1f), ForceMode.Impulse);
	}

	public override void Update()
	{
		TTL += Time.deltaTime;

		if (TTL >= Range)
		{
			if (Explodes == true)
			{
				Explosion();
			}

			TTL = 0;
			gameObject.SetActive(false);
		}
	}

	public override void OnCollisionEnter(Collision collision)
	{

	}
}