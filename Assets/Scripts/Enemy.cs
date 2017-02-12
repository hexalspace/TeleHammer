using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Enemy : MonoBehaviour, 
	Receiver<Message.HammerAttack>,
	Receiver<Message.PlayerAttack>
{
	public void receive ( HammerAttack weaponCollision, GameObject sender )
	{
		if ( weaponCollision.hitObject == gameObject )
		{
			Vector3 killedPosition = new Vector3();
			killedPosition = gameObject.transform.position;
			gameObject.sendMessage( new EnemyKilledByHammer() { enemyKillLocation = killedPosition } );
			Debug.Log( "Enemy Killed" );
			Destroy( gameObject );
		}
	}

	public void receive ( PlayerAttack o, GameObject sender )
	{
		if ( o.hitObject == gameObject )
		{
			Debug.Log( "Enemy Killed" );
			Destroy( gameObject );
		}
	}
}