using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CollisionEnableComponent
{
	Collider[] getCollisions ();

}

public interface CollideComponent
{
	void collide (GameObject other);
}

public class CollisionSystem
{
	public void run ( GameObject[] gameObjects )
	{
		foreach (var gameObject in gameObjects )
		{
			var collisionComponent = gameObject.GetComponent<CollisionEnableComponent>();

			if (collisionComponent == null)
			{
				continue;
			}

			var collideComponent = gameObject.GetComponent<CollideComponent>();

			if (collideComponent == null)
			{
				continue;
			}

			foreach(var collider in collisionComponent.getCollisions())
			{
				var hitGameObject = collider.gameObject;

				collideComponent.collide( hitGameObject );

				Debug.Log( "COLLISION" );
			}
		}
	}
}
