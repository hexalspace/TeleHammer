using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollisionComponent
{
	Collider[] getCollisions();
}

public class CollisionSystem
{
	public void run ( GameObject[] gameObjects )
	{
		foreach (var gameObject in gameObjects )
		{
			var collisionComponent = gameObject.GetComponent<CollisionComponent>();

			if (collisionComponent == null)
			{
				continue;
			}

			foreach(var collisionContainer in collisionComponent.getCollisions())
			{
				Debug.Log( "COLLISION" );
			}
		}
	}
}
