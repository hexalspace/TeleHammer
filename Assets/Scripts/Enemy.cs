using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Enemy : MonoBehaviour, 
	Receiver<Message.WeaponCollision>
{
	public void receive ( WeaponCollision weaponCollision, GameObject sender )
	{
		if ( weaponCollision.hitObject == gameObject )
		{
			Destroy( gameObject );
		}
	}
}