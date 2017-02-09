using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifetime : MonoBehaviour, Receiver {

	public void receive ( object o, Type t, GameObject g )
	{
		if ( t == typeof( Message.WeaponCollision ) )
		{
			Destroy( gameObject );
		}
	}
}
