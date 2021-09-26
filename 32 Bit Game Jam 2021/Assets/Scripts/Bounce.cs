using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
	[SerializeField] private float bounceForce = 35;
    [SerializeField]
    private AudioSource bounceSource;

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerMovement>() == true)
		{
			other.GetComponent<PlayerMovement>().AddForce(Vector3.up * bounceForce);
            bounceSource.Play();
		}
	}
}
