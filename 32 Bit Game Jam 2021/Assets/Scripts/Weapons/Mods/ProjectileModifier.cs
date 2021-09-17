using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileModifier : ScriptableObject
{
	protected Projectile AttachedProjectile;

	public virtual void Initialize(Projectile _attachedProjectile)
	{
		AttachedProjectile = _attachedProjectile;
	}

	public virtual void Update()
	{

	}

	public virtual void OnHit()
	{

	}
}
