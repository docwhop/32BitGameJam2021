using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
	public float TTL;
	float timer;

    void Update()
    {
		timer += Time.deltaTime;

		if(timer >= TTL)
		{
			Destroy(gameObject);
		}
    }
}
