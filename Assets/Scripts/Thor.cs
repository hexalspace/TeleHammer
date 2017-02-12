using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;


public class Thor : MonoBehaviour,
	Receiver<Message.EnemyKilledByHammer>
{
	enum ThorState
	{
		DEFAULT,
		TELEPORTING,
	}

	public float teleportTime = 1.0f;

	private ThorState thorState;
	private Vector3 teleportFrom;
	private Vector3 teleportTo;
	private float elapsedTeleportTime = 0.0f;

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		if ( thorState == ThorState.DEFAULT )
		{
		}
		else if (thorState == ThorState.TELEPORTING)
		{
			elapsedTeleportTime += Time.deltaTime;
			float percentagePickupTime = Mathf.Clamp01( elapsedTeleportTime / teleportTime );

			transform.position = Vector3.Lerp( teleportFrom, teleportTo, percentagePickupTime );

			if ( elapsedTeleportTime >= teleportTime )
			{
				thorState = ThorState.DEFAULT;
			}
		}
	}

	public void receive ( EnemyKilledByHammer o, GameObject sender )
	{
		thorState = ThorState.TELEPORTING;
		teleportFrom = gameObject.transform.position;
		teleportTo = o.enemyKillLocation;
		elapsedTeleportTime = 0.0f;
	}

	void OnCollisionEnter ( Collision collision )
	{
		if ( thorState == ThorState.TELEPORTING )
		{
			gameObject.sendMessage( new Message.PlayerAttack() { hitObject = collision.gameObject } );
		}
	}

	void OnTriggerEnter ( Collider collider )
	{
		if ( thorState == ThorState.TELEPORTING )
		{
			gameObject.sendMessage( new Message.PlayerAttack() { hitObject = collider.gameObject } );
		}
	}

}
