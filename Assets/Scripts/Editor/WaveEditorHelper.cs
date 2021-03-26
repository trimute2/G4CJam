using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaveSpawner))]
public class WaveEditorHelper : Editor
{
	private void OnSceneGUI()
	{
		Wave[] waves = ((WaveSpawner)target).waves;
		for (int i = 0; i < waves.Length; i++)
		{
			Wave wave = waves[i];
			for(int j = 0; j < wave.enemies.Length; j++)
			{
				WaveEnemies enemy = wave.enemies[j];

				enemy.Position = Handles.PositionHandle(enemy.Position, Quaternion.identity);
				switch (enemy.color)
				{
					case BasicEnemy.EnemyColor.red:
						Handles.color = Color.red;
						break;
					case BasicEnemy.EnemyColor.green:
						Handles.color = Color.green;
						break;
					case BasicEnemy.EnemyColor.blue:
						Handles.color = Color.blue;
						break;
				}

				if (Handles.Button(enemy.Position, Quaternion.identity, 0.5f, 0.5f, Handles.DotHandleCap))
				{
					enemy.color = (BasicEnemy.EnemyColor)((((int)enemy.color)+1)%3);
				}

				Handles.Label(enemy.Position, i.ToString());
				wave.enemies[j] = enemy;
			}
			waves[i] = wave;
		}
		((WaveSpawner)target).waves = waves;
	}
}
