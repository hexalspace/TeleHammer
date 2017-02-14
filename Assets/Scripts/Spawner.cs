using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Spawner : MonoBehaviour,
	Receiver<Message.EnemyKilled>
{

	private int enemiesSpawned = 1;
	private int enemiesKilled = 0;

	public GameObject[] enemyPrefabs;

	public GameObject thor;

	public void receive ( EnemyKilled o, GameObject sender )
	{
		enemiesKilled += 1;
		if (enemiesKilled == enemiesSpawned)
		{
			enemiesKilled = 0;
			enemiesSpawned *= 2;

			foreach (var i in System.Linq.Enumerable.Range( 0, enemiesSpawned ))
			{
				spawnOne();
			}
		}
	}

	public void spawnOne()
	{
		var toSpawn = enemyPrefabs[UnityEngine.Random.Range( 0, enemyPrefabs.Length )];
		var enemy = Instantiate( toSpawn, Vector3.zero, Quaternion.identity );
		var enemyAI = enemy.GetComponent<EnemyAI>();
		enemyAI.player = thor.transform;
	}

	// Use this for initialization
	void Start ()
	{
		spawnOne();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
