using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	[SerializeField] int damage;

	[SerializeField] float speed;

	[SerializeField] float min;
	[SerializeField] float max;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

	bool canDamage;

	Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
        source.PlayOneShot(clip);
    }

    public void Initialize(Vector3 pos, int dmg, float sped, float minSize, float maxSize)
	{
		damage = dmg;

		speed = sped;

		min = minSize;
		max = maxSize;

		transform.position = pos;
		transform.rotation = Quaternion.identity;
		transform.localScale = new Vector3(min, min, min);


        canDamage = true;
	}

	void Update()
	{
		if(transform.localScale.x < max)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(max, max, max), speed * Time.deltaTime);
		}
		else
		{
			gameObject.SetActive(false);
		}

		if(canDamage == true)
		{
			col.enabled = true;
			canDamage = false;
		}
		else
		{
			col.enabled = false;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent(out Health hitHealth) == true)
		{
			hitHealth.Damage(damage);
		}
	}
}
