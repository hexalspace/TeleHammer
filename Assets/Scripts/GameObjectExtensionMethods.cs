using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensionMethods
{
	public static void sendMessage<T> ( this UnityEngine.GameObject gameObject, T message )
	{
		var messenger = gameObject.GetComponent<Messenger>();
		if (messenger == null)
		{
			Debug.Log( "Sending objects should have a messenger component" );
			return;
		}

		messenger.sendMessage( message );
		return;
	}
}