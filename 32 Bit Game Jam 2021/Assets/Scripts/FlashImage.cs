using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashImage : MonoBehaviour
{
	public float Duration;

	float timer;

	bool on;

	Image damageIndicator;

	void Awake()
	{
		damageIndicator = GetComponentInChildren<Image>();
	}

	void Update()
    {
        if(on == true)
		{
			timer += Time.deltaTime;

			if(timer >= Duration)
			{
				timer = 0;
				on = false;
			}
		}
		else
		{
			damageIndicator.enabled = false;
		}
    }

	public void Flash()
	{
		damageIndicator.enabled = true;
		on = true;
	}
}
