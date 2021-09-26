using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	public Health Health;

	public Sprite FullHP;
	public Sprite EmptyHP;

	Image[] hearts;

    void Awake()
    {
		hearts = new Image[transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
		{
			hearts[i] = transform.GetChild(i).GetComponent<Image>();

			if(i >= Health.MaxHP)
			{
				hearts[i].gameObject.SetActive(false);
			}
		}
    }

	void Update()
	{
		for (int i = 0; i < hearts.Length; i++)
		{
			if(i < Health.MaxHP)
			{
				if (i < Health.HP)
				{
					hearts[i].sprite = FullHP;
				}
				else
				{
					hearts[i].sprite = EmptyHP;
				}

				hearts[i].gameObject.SetActive(true);
			}
			else
			{
				hearts[i].gameObject.SetActive(false);
			}
		}	
	}
}
