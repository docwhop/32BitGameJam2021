using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]

public class Actor : MonoBehaviour
{
	[HideInInspector] public Health Health;

	private NavMeshAgent navAgent;

	private void Awake()
	{
		Health = GetComponent<Health>();

		navAgent = GetComponent<NavMeshAgent>();

		//Sets up actor callbacks and HP values
		Health.AddDamageCallback(OnDamage);
		Health.AddDeathCallback(OnDeath);
	}

	private void Update()
	{
		//DEBUG: Walk toward player
		navAgent.SetDestination(Camera.main.transform.position);
	}

	protected virtual void OnDamage()
	{

	}

	protected virtual void OnDeath()
	{

	}
}
