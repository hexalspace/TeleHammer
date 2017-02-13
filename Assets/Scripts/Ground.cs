using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Ground : MonoBehaviour
{
	void OnCollisionEnter ( Collision collision )
	{
		gameObject.sendMessage( new GroundCollision() { hitObject = collision.gameObject } );
	}

	void OnTriggerEnter ( Collider collider )
	{
		gameObject.sendMessage( new GroundCollision() { hitObject = collider.gameObject } );
	}
}
