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

	public float minDistanceFromPlayer;
	public float minXpos;
	public float maxXpos;
	public float minYpos;
	public float maxYpos;
	public float minZpos;
	public float maxZpos;
	public Transform LowerLeftBounds;

	public GameObject[] enemyPrefabs;

	public GameObject thor;

	public void receive ( EnemyKilled o, GameObject sender )
	{
		enemiesKilled += 1;
		if (enemiesKilled == enemiesSpawned)
		{
			enemiesKilled = 0;
			enemiesSpawned += 1;

			foreach (var i in System.Linq.Enumerable.Range( 0, enemiesSpawned ))
			{
				spawnOne();
			}
		}
	}

	public void spawnOne()
	{
		bool foundSpawnLocation = false;
		var whereToSpawn = new Vector3();
		while (!foundSpawnLocation )
		{
			var xSpawnPos = UnityEngine.Random.Range( minXpos, maxXpos );
			var ySpawnPos = UnityEngine.Random.Range( minYpos, maxYpos );
			var zSpawnPos = UnityEngine.Random.Range( minZpos, maxZpos );
			whereToSpawn = new Vector3( xSpawnPos, ySpawnPos, zSpawnPos );

			// Must be certain distance from player to spawn
			foundSpawnLocation = Vector3.Distance( whereToSpawn, thor.transform.position ) > minDistanceFromPlayer;
		}


		var toSpawn = enemyPrefabs[UnityEngine.Random.Range( 0, enemyPrefabs.Length )];
		var enemy = Instantiate( toSpawn, whereToSpawn, Quaternion.identity );
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
