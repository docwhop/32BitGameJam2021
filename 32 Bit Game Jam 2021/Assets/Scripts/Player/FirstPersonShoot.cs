using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonShoot : MonoBehaviour
{
    public Camera camera;
    public GameObject projectile;
    public Transform leftFirePoint, rightFirePoint;

    [SerializeField]
    float projectileSpeed = 30f;
    [SerializeField]
    float fireRate = 4;


    private Vector3 destination;
    private bool leftHand;
    private float timeToFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        if (leftHand)
        {
            InstantiateProjectile(leftFirePoint);
            leftHand = false;
        }
        else
        {
            InstantiateProjectile(rightFirePoint);
            leftHand = true;
        }
    }

    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
    }


}
