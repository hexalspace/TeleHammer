using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCollisionComponent : MonoBehaviour, CollisionEnableComponent
{
	// Use this for initialization
	void Start ()
	{
		enteredCollisions = new List<Collider>();
		var r = gameObject.AddComponent<Rigidbody>();
		r.isKinematic = true;
	}

	private List<Collider> enteredCollisions;

	public Collider[] getCollisions ()
	{
		var collisions = enteredCollisions.ToArray();
		enteredCollisions.Clear();
		return collisions;
	}

	void OnTriggerEnter ( Collider collider )
	{
		enteredCollisions.Add( collider );
	}
}