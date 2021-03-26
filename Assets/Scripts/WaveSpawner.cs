using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

	public Wave[] waves;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct Wave
{
	public int EnemiesToKillForWaveToStart;
	public WaveEnemies[] enemies;
}

[System.Serializable]
public struct WaveEnemies
{
	public Vector3 Position;
	public BasicEnemy.EnemyColor color;
}