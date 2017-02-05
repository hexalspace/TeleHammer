using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDeathComponent : MonoBehaviour, DeathComponent
{
	public void kill ()
	{
		GameObject.Destroy( this.gameObject );
	}
}
