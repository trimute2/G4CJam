using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
	public GameObject disapearEffect;
	private void OnTriggerEnter(Collider collision)
	{
		PlayerScript player = collision.GetComponent<PlayerScript>();
		if(player != null)
		{
			player.health++;
			Instantiate(disapearEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
