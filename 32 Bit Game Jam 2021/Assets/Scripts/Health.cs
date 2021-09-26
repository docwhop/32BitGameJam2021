using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	public int HP { get { return hp; } }
	[SerializeField] private int hp;

	public int MaxHP { get { return maxHp; } }
	[SerializeField] private int maxHp;

	public UnityEvent OnDamage;

	public UnityEvent OnDeath;

	public void AddDeathCallback(UnityAction _onDeath)
	{
		OnDeath.AddListener(_onDeath);
	}

	public void AddDamageCallback(UnityAction _onDamage)
	{
		OnDamage.AddListener(_onDamage);
	}

	public void Damage(int _amount)
	{
		hp -= _amount;

		ClampHP();

		//If damage callback isnt null, invoke
		OnDamage?.Invoke();

		if (hp <= 0)
		{
			//If death callback isnt null, invoke
			OnDeath?.Invoke();
            EventManager.Instance.PlayerDied();
		}
	}

	public void Heal(int _amount)
	{
		hp += _amount;

		ClampHP();
	}

	public void FullHP()
	{
		hp = maxHp;
	}

	void ClampHP()
	{
		//Stop hp going over the MaxHp or under 0
		hp = Mathf.Clamp(hp, 0, maxHp);
	}
}