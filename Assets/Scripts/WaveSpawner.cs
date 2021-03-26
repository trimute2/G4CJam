using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

	public GameObject EnemyPrefab;
	public Wave[] waves;
	public int currentWave;

    // Start is called before the first frame update
    void Start()
    {
		SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SpawnWave()
	{
		foreach(WaveEnemies enemy in waves[currentWave].enemies)
		{
			GameObject enemySpawning = Instantiate(EnemyPrefab, enemy.Position, Quaternion.identity);
			BasicEnemy enemyCode = enemySpawning.GetComponent<BasicEnemy>();
			enemyCode.ChangeColor(enemy.color);
		}
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