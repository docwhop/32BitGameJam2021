using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	[HideInInspector] public Health Health;

	[HideInInspector] public Rigidbody Rbody;

	[HideInInspector] public Collider Collider;

	[HideInInspector] public WeaponHandler WeaponHandler;

	public ActorData Data;

	public ActorController Controller;

	public Transform[] GunEnds;

	void Awake()
    {
		Health = GetComponent<Health>();

		Rbody = GetComponent<Rigidbody>();

		Collider = GetComponent<Collider>();

		WeaponHandler = GetComponent<WeaponHandler>();

		Health.AddDamageCallback(OnDamage);
		Health.AddDeathCallback(OnDeath);

		Initialize(Data, Controller);
    }

	public void Initialize(ActorData _data, ActorController _controller)
	{
		Data = _data;
		Controller = _controller;

		Health.FullHP();

		Controller.Initialize(this);
	}

	void Update()
	{
		Controller.Update();	
	}

	void FixedUpdate()
	{
		Controller.FixedUpdate();
	}

	void OnCollisionEnter(Collision collision)
	{
		Controller.OnCollisionEnter(collision);	
	}

	void OnDamage()
	{
		//When shot calls this method
	}

	void OnDeath()
	{
		//When killed calls this method
		gameObject.SetActive(false);
	}

	public void ControllerAnimEvent() //This sucks but dont have time for proper solution
	{
		Controller.AnimEvent();
	}
}
