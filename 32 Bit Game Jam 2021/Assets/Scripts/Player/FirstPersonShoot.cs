using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShoot : MonoBehaviour
{
	Camera cam;

	WeaponHandler weaponHandler;

	public Transform[] FirePoints;
	int firePointIndex;

	Collider col;

    private void OnEnable()
    {
        //EventManager.weaponChangedEvent += AssignFirePoints; 
    }

    private void OnDisable()
    {
        //EventManager.weaponChangedEvent -= AssignFirePoints;
    }

    void Awake()
	{
		cam = GetComponentInChildren<Camera>();

		weaponHandler = GetComponent<WeaponHandler>();

		col = GetComponent<CharacterController>();
	}

	void Update()
    {
        if (Input.GetButton("Fire1") && !GameManager.Instance.IsGamePaused)
        {
			if(weaponHandler.FirePrimary(FirePoints[firePointIndex].position, ProjectileDirection(), col) == true)
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

    //public void AssignFirePoints()
    //{
    //    // Each gun has its own child game object called "Projectile spawn point" which defines where the projectile will shoot from on the weapon model. 
    //    // Find and assign it here.
    //}
}
