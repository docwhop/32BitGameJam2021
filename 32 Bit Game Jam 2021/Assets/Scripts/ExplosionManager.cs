using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
	public static ExplosionManager Instance;

	public GameObject ExplosionPrefab;

	public int Size;

	Explosion[] pool;

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

		pool = new Explosion[Size];

		for (int i = 0; i < pool.Length; i++)
		{
			pool[i] = Instantiate(ExplosionPrefab, transform).GetComponent<Explosion>();

			pool[i].gameObject.SetActive(false);
		}
    }

	public void SpawnExplosion(Vector3 pos, int dmg, float sped, float min, float max)
	{
		for (int i = 0; i < pool.Length; i++)
		{
			if(pool[i].gameObject.activeSelf == false)
			{
				pool[i].Initialize(pos, dmg, sped, min, max);
				pool[i].gameObject.SetActive(true);
			}
		}
	}
}
