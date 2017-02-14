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
			gameObject.sendMessage( new EnemyKilled() );
			gameObject.sendMessage( new EnemyKilledByHammer() { enemyKillLocation = killedPosition } );
			Debug.Log( "Enemy Killed" );
			Destroy( gameObject );
		}
	}

	public void receive ( PlayerAttack o, GameObject sender )
	{
		if ( o.hitObject == gameObject )
		{
			gameObject.sendMessage( new EnemyKilled() );
			Debug.Log( "Enemy Killed" );
			Destroy( gameObject );
		}
	}

	void OnCollisionEnter ( Collision collision )
	{
		gameObject.sendMessage( new EnemyAttack() { hitObject = collision.gameObject } );
	}

	void OnTriggerEnter ( Collider collider )
	{
		gameObject.sendMessage( new EnemyAttack() { hitObject = collider.gameObject } );
	}
}