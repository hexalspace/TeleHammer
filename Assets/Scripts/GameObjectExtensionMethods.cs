using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensionMethods
{
	private static MessageSystem messageSystem;
	private static GameObject messageSystemGameObject;

	public static MessageSystem GetMessageSystem ()
	{
		if ( messageSystemGameObject == null)
		{
			messageSystemGameObject = new GameObject();
			messageSystemGameObject.name = "Global Message System";
			messageSystem = messageSystemGameObject.AddComponent<MessageSystem>();
			messageSystem.Init();
		}
		return messageSystem;
	}

	public static GameObject CreateObject(GameObject original, Vector3 position, Quaternion rotation = new Quaternion(), Transform parent = null)
    {
		return GetMessageSystem().CreateObject(original, position, rotation, parent);
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
		GetMessageSystem().sendMessage( message, typeof(T), gameObject );
		return;
	}
}