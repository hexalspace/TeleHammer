using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		var r = gameObject.AddComponent<Rigidbody>();
		r.isKinematic = true;
	}

	void OnTriggerEnter ( Collider collider )
	{
		collider.gameObject.sendMessage( new Message.EnemyCollision() );
	}
}