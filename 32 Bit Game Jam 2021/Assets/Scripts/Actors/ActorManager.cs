using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
	public static ActorManager Instance;

	Actor[] pool;

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

		pool = new Actor[transform.childCount];

		for (int i = 0; i < pool.Length; i++)
		{
			pool[i] = transform.GetChild(i).GetComponent<Actor>();
		}
	}
}
