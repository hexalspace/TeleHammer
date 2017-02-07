using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		var r = gameObject.AddComponent<Rigidbody>();
		r.isKinematic = true;
	}

	void OnTriggerEnter ( Collider collider )
	{
		gameObject.sendMessage( collider );
	}
}