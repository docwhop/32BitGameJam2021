using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShoot : MonoBehaviour
{
	Camera cam;

	WeaponHandler weaponHandler;

	public Transform[] FirePoints;
	int firePointIndex;

	private void Awake()
	{
		cam = GetComponentInChildren<Camera>();

		weaponHandler = GetComponent<WeaponHandler>();
	}

	void Update()
    {
        if (Input.GetButton("Fire1"))
        {
			if(weaponHandler.FirePrimary(FirePoints[firePointIndex].position, ProjectileDirection()) == true)
			{
				firePointIndex++;

				if (firePointIndex >= FirePoints.Length)
				{
					firePointIndex = 0;
				}
			}
		}
    }

	Vector3 ProjectileDirection()
	{
		Ray projectileRay = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));

		Vector3 destination = projectileRay.GetPoint(1000);

		if (Physics.Raycast(projectileRay, out RaycastHit projectileHitInfo))
		{
			destination = projectileHitInfo.point;
		}

		return (destination - FirePoints[firePointIndex].position).normalized;
	}
}
