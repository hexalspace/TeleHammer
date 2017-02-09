using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Receiver
{
	void receive ( object o, System.Type type, GameObject sender );
}

public class MessageSystem : MonoBehaviour
{
	private class Message
	{
		public object o;
		public System.Type type;
		public GameObject sender;
	}

	private Queue<Message> messageQueue;

    void Start ()
	{
		Init();
	}

	public void Init ()
	{
		if (messageQueue == null)
		{
			messageQueue = new Queue<Message>();
		}
	}

	public void sendMessage(object o, System.Type type, GameObject sender)
	{
		messageQueue.Enqueue( new Message { o = o, type = type, sender = sender } );
	}
	
	// Update is called once per frame
	void Update ()
	{
		var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
		var receivers = new List<Receiver>();

		foreach (var gameObject in gameObjects)
		{
			receivers.AddRange( gameObject.GetComponents<Receiver>() );
		}

		while (messageQueue.Count > 0)
		{
			var message = messageQueue.Dequeue();
			foreach (var receiver in receivers )
			{
				receiver.receive( message.o, message.type, message.sender );
			}
		}
	}
}
