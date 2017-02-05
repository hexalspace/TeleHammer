﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HealthComponent
{
	bool isAlive ();
}

public interface DeathComponent
{
	void kill ();
}


public class LifetimeSystem
{
	public void run(GameObject[] gameObjects)
	{
		foreach(var gameObject in gameObjects)
		{
			var healthComponent = gameObject.GetComponent<HealthComponent>();

			if ( healthComponent != null)
			{
				if ( !healthComponent.isAlive())
				{
					var deathComponent = gameObject.GetComponent<DeathComponent>();
					if (deathComponent != null)
					{
						deathComponent.kill();
					}
				}
			}
		}
	}
}
