using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnModifier : ProjectileModifier
{
	[SerializeField] private GameObject Object;

	[SerializeField] private bool spawnOnInitialize;

	[SerializeField] private bool spawnOnTimer;
	[SerializeField] private float spawnRate;
	private float timer;

	[SerializeField] private bool SpawnOnHit;
	//[SerializeField] private LayerMask hitLayers;

	public override void Initialize(Projectile _attachedProjectile)
	{
		base.Initialize(_attachedProjectile);

		if(spawnOnInitialize == true)
		{
			Instantiate(Object, AttachedProjectile.transform.position, Quaternion.identity);
		}
	}

	public override void Update()
	{
		base.Update();

		if(spawnOnTimer == true)
		{
			timer += Time.deltaTime;

			if(timer >= spawnRate)
			{
				Instantiate(Object, AttachedProjectile.transform.position, Quaternion.identity);

				timer = 0;
			}
		}
	}

	public override void OnHit()
	{
		base.OnHit();

		if(SpawnOnHit == true)
		{
			Instantiate(Object, AttachedProjectile.transform.position, Quaternion.identity);
		}
	}
}
