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

    public bool IsDead { get; set; }

	public GameObject Bleed;

	void Awake()
    {
		Health = GetComponent<Health>();

		Rbody = GetComponent<Rigidbody>();

		Collider = GetComponent<Collider>();

		WeaponHandler = GetComponent<WeaponHandler>();

		Health.AddDamageCallback(OnDamage);
		Health.AddDeathCallback(OnDeath);

		Initialize(Instantiate(Data), Instantiate(Controller));
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
		Rbody.velocity = Vector3.zero;
		Rbody.angularVelocity = Vector3.zero;

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
		Instantiate(Bleed, Collider.bounds.center, Quaternion.identity);
	}

	void OnDeath()
	{
        Controller.DeathEvent();
        IsDead = true;
		//gameObject.SetActive(false); 
	}

	//This sucks but dont have time for proper solution
	public void AttackEvent()
	{
		Controller.AttackEvent();
	}

	public void LightAttackEvent()
	{
		Controller.LightAttackEvent();
	}

	public void HeavyAttackEvent()
	{
		Controller.HeavyAttackEvent();
	}
}
