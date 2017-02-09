using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Receiver<T>
{
	void receive ( T o, GameObject sender );
}

public class MessageSystem : MonoBehaviour
{
	private class Message
	{
		public object o;
		public System.Type type;
		public GameObject sender;
	}

	private class ReceiverTypeInfo
	{
		public System.Type receiveType;
		public System.Reflection.MethodInfo receiverTypeReceiveMethod;
	}

	private static readonly string receiverMethodName = "receive";

	private Queue<Message> messageQueue;
	private Dictionary<System.Type, ReceiverTypeInfo> methodsByType;


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

		if ( methodsByType == null)
		{
			methodsByType = new Dictionary<System.Type, ReceiverTypeInfo>();
		}
	}

	public void sendMessage(object o, System.Type type, GameObject sender)
	{
		messageQueue.Enqueue( new Message { o = o, type = type, sender = sender } );
	}
	
	private ReceiverTypeInfo GetReceiveTypeInfo (System.Type t)
	{
		if (!methodsByType.ContainsKey(t))
		{
			var receiverTypeInfo = new ReceiverTypeInfo();
			receiverTypeInfo.receiveType = typeof( Receiver<> ).MakeGenericType( t );
			receiverTypeInfo.receiverTypeReceiveMethod = receiverTypeInfo.receiveType.GetMethod( receiverMethodName );
			methodsByType[t] = receiverTypeInfo;
		}

		return methodsByType[t];
	}
	
	// Update is called once per frame
	void Update ()
	{
		var gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();

		while (messageQueue.Count > 0)
		{
			var message = messageQueue.Dequeue();
			var receiverTypeInfo = GetReceiveTypeInfo( message.type );
			foreach ( var gameObject in gameObjects )
			{
				foreach (var receiver in gameObject.GetComponents( receiverTypeInfo.receiveType ))
				{
					receiverTypeInfo.receiverTypeReceiveMethod.Invoke(receiver, new object[] { message.o, message.sender } );
				}
			}
		}
	}
}
