using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour {

	LifetimeSystem lifetimeSystem;
	CollisionSystem collisionSystem;

	// Use this for initialization
	void Start () {
		collisionSystem = new CollisionSystem();
		lifetimeSystem = new LifetimeSystem();
	}
	
	// Update is called once per frame
	void Update () {
		var gameObjects = FindObjectsOfType<GameObject>();

		if (gameObjects == null || gameObjects.Length == 0)
		{
			return;
		}

		collisionSystem.run( gameObjects );
		lifetimeSystem.run( gameObjects );
	}
}
