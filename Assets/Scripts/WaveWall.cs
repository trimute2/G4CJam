using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveWall : MonoBehaviour
{
	public WaveSpawner spawner;

	public int waveNumber;

	private void Start()
	{
		spawner.OnWaveEnd += RespondToWaveEnd;
	}

	public void RespondToWaveEnd(int wave)
	{
		if(wave == waveNumber)
		{
			spawner.OnWaveEnd -= RespondToWaveEnd;
			Destroy(gameObject);
		}
	}
}
