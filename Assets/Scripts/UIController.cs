using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

	public Image[] ammo;
	public Image shieldIcon;
	public Image nextShieldIcon;

	public Slider healthSlider;

    public void UpdateAmmo(int ammoCount)
	{
		if(ammoCount < 0)
		{
			return;
		}

		for(int i = 0; i < ammo.Length; i++)
		{
			Color paint = Color.white;
			if(i < ammoCount)
			{
				paint = Color.red;
			}
			ammo[i].color = paint;
		}
	}

	public void UpdateShield(BasicEnemy.EnemyColor shieldColor)
	{
		Color c1 = Color.white;
		Color c2 = Color.white;
		switch (shieldColor)
		{
			default:
			case BasicEnemy.EnemyColor.red:
				c1 = Color.red;
				c2 = Color.green;
				break;
			case BasicEnemy.EnemyColor.green:
				c1 = Color.green;
				c2 = Color.blue;
				break;
			case BasicEnemy.EnemyColor.blue:
				c1 = Color.blue;
				c2 = Color.red;
				break;
		}
		shieldIcon.color = c1;
		nextShieldIcon.color = c2;
	}

	public void UpdateHealth(int health)
	{
		healthSlider.value = Mathf.Min(3, health);
	}

}
