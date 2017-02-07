using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		var r = gameObject.AddComponent<Rigidbody>();
		r.isKinematic = true;
	}

	void OnTriggerEnter ( Collider collider )
	{
		if (collider.gameObject.tag == "Weapon")
		{
			gameObject.sendMessage( new Message.WeaponCollision() );
		}
	}
}