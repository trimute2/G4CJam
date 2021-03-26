using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemAutoDestroy : MonoBehaviour
{
	ParticleSystem partSystem;
    // Start is called before the first frame update
    void Start()
    {
		partSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!partSystem.IsAlive())
		{
			Destroy(gameObject);
		}
    }
}
