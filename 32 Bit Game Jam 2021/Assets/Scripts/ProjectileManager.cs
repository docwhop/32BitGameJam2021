using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
	public static ProjectileManager Instance;

	[SerializeField] private Projectile prefab;

	[SerializeField] private int size;

	Projectile[] pool;

	void Awake()
    {
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}

		pool = new Projectile[size];

		for (int i = 0; i < pool.Length; i++)
		{
			pool[i] = Instantiate(prefab.gameObject, transform).GetComponent<Projectile>();
			pool[i].gameObject.SetActive(false);
		}
	}

	public void SpawnProjectile(Vector3 _position, Vector3 _direction, float _speed, float _range, int _damage, ProjectileModifier[] _modifiers)
	{
		for (int i = 0; i < pool.Length; i++)
		{
			if(pool[i].gameObject.activeSelf == false)
			{
				pool[i].Initialize(_position, _direction, _speed, _range, _damage, _modifiers);
				pool[i].gameObject.SetActive(true);

				break;
			}
		}
	}
}
