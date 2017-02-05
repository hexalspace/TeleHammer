using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSystem
{
	void OnCollisionEnter ( Collision collision )
	{
		Debug.Log( "HIT" );
	}
}
