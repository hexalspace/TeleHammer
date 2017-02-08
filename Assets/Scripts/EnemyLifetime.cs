using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetime : MonoBehaviour, Receiver {

	public void receive ( Type t, object o )
	{
		if ( t == typeof( Message.WeaponCollision ) )
		{
			Destroy( gameObject );
		}
	}
}
