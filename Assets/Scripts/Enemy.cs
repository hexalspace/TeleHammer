using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Enemy : MonoBehaviour, 
	Receiver<Message.HammerCollision>
{
	public void receive ( HammerCollision weaponCollision, GameObject sender )
	{
		if ( weaponCollision.hitObject == gameObject )
		{
			Vector3 killedPosition = new Vector3();
			killedPosition = gameObject.transform.position;
			gameObject.sendMessage( new EnemyKilledByHammer() { enemyKillLocation = killedPosition } );
			Destroy( gameObject );
		}
	}
}