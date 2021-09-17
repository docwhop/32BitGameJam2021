using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerMovement>() == true)
		{
			other.GetComponent<PlayerMovement>().AddForce(Vector3.up * 15);
		}
	}
}
