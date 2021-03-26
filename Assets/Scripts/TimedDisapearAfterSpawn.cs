using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDisapearAfterSpawn : MonoBehaviour
{

	public float DisapearTime;

    // Update is called once per frame
    void Update()
    {
		DisapearTime -= Time.deltaTime;
		if(DisapearTime <= 0)
		{
			Destroy(gameObject);
		}
    }
}
