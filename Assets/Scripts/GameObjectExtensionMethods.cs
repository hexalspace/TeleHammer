using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensionMethods
{
	private static Messenger getMessenger ( this UnityEngine.GameObject gameObject )
	{
		var messenger = gameObject.GetComponent<Messenger>();

		if ( messenger == null )
		{
			messenger = gameObject.AddComponent<Messenger>();
			messenger.initialize();
		}

		return messenger;
	}

	public static T AddOrGetComponent<T> ( this UnityEngine.GameObject gameObject ) where T : UnityEngine.Component
	{
		var component = gameObject.GetComponent<T>();
		if (component == null)
		{
			component = gameObject.AddComponent<T>();
		}
		return component;
	}

	public static void sendMessage<T> ( this UnityEngine.GameObject gameObject, T message )
	{
		gameObject.getMessenger().sendMessage( message );
		return;
	}

	public static void addSubscriber ( this UnityEngine.GameObject gameObject, GameObject subscriber )
	{
		gameObject.getMessenger().addSubscriber( subscriber );
		return;
	}

	public static void removeSubscriber ( this UnityEngine.GameObject gameObject, GameObject subscriber )
	{
		gameObject.getMessenger().removeSubscriber( subscriber );
		return;
	}
}