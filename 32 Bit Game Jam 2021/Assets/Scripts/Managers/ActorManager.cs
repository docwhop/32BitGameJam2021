using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
	public static ActorManager Instance { get { return instance; } }
	private static ActorManager instance;

	Actor[] actors;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}

		actors = new Actor[transform.childCount];

		for (int i = 0; i < actors.Length; i++)
		{
			actors[i] = transform.GetChild(i).GetComponent<Actor>();
		}
	}
}
