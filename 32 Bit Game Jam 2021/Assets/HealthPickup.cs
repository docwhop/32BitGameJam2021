using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private AudioSource healthSource;
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			Health health = other.GetComponent<Health>();

			if(health.HP < health.MaxHP)
			{
				other.GetComponent<Health>().Heal(1);
                healthSource.Play();
				Destroy(gameObject);
			}
		}
	}
}
