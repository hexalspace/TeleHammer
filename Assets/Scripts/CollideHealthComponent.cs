using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideHealthComponent : MonoBehaviour, HealthComponent, CollideComponent {

	public bool hasBeenHit = false;

	public void collide ( GameObject other )
	{
		if (other.tag == "Weapon")
		{
			hasBeenHit = true;
		}
	}

	public bool isAlive ()
	{
		return !hasBeenHit;
	}

}
