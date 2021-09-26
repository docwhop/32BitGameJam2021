using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechHealthUI : MonoBehaviour
{
	public Health MechHealth;

	public Image HPBar;
	float startScale;

	private void Awake()
	{
		startScale = HPBar.rectTransform.rect.width;
	}

	void Update()
    {
		if(Vector3.Distance(Camera.main.transform.position, MechHealth.transform.position) <= 80)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}

			HPBar.transform.localScale = new Vector3((float)MechHealth.HP / (float)MechHealth.MaxHP, 1, 1);		
		}
		else
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
    }
}
