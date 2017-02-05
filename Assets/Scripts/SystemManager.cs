using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour {

	LifetimeSystem lifetimeSystem;

	// Use this for initialization
	void Start () {
		lifetimeSystem = new LifetimeSystem();
	}
	
	// Update is called once per frame
	void Update () {
		var gameObjects = FindObjectsOfType<GameObject>();
		lifetimeSystem.run( gameObjects );
	}
}
