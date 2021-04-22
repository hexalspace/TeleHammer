using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public abstract class MonoReceiver<T> : MonoBehaviour
{
    private void Awake()
    {
		// Register for receiving messages here
		GameObjectExtensionMethods.GetMessageSystem().RegisterReceiver<T>(this);
	}

	private void OnDestroy()
    {
		// Deregister for receiving messages here
	}

	public abstract void receive(T o, GameObject sender);
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
	private Dictionary<System.Type, HashSet<System.Type>> typeToReciever = new Dictionary<System.Type, HashSet<System.Type>>();
	private Dictionary<System.Type, GameObject> messageToReceivers = new Dictionary<System.Type, GameObject>();

	void Start ()
	{
		Init();
	}

	public void RegisterReceiver<T>(MonoReceiver<T> receiver)
    {
		Action<string> greet = name =>
		{
			string greeting = $"Hello {name}!";
			Console.WriteLine(greeting);
		};

		Action<GameObject, T> sendAction = (go, message)  =>
		{
			receiver.receive(message, go);
		};
    }

	public GameObject CreateObject(GameObject original, Vector3 position, Quaternion rotation = new Quaternion(), Transform parent = null)
	{
		var gameObj = Instantiate(original, position, rotation, parent);
		var comps = gameObj.GetComponents(typeof(Component));

		foreach(var c in comps)
        {
			HashSet<System.Type> x;
			if (typeToReciever.TryGetValue(c.GetType(), out x))
            {
				foreach (var reciever in x)
                {

                }
            }
        }


		return gameObj;
	}

	public void Init ()
	{
		// Get all Message types
		var recieverTypes = new List<ReceiverTypeInfo>();
		var messageTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "Message");
		foreach(var m in messageTypes)
        {
			// Construct all possible Reciever interfaces
			var receiverTypeInfo = new ReceiverTypeInfo();
			receiverTypeInfo.receiveType = typeof(Receiver<>).MakeGenericType(m);
			receiverTypeInfo.receiverTypeReceiveMethod = receiverTypeInfo.receiveType.GetMethod(receiverMethodName);
			recieverTypes.Add(receiverTypeInfo);
		}

		foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
        {
			typeToReciever[t] = new HashSet<System.Type>();
			foreach (var recieverType in recieverTypes)
            {
				if (t.GetInterfaces().Contains(recieverType.receiveType))
				{
					typeToReciever[t].Add(recieverType.receiveType);
				}
			}

        }

		// Create easy insert/remove gameObject collections for each reciever type


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
		while (messageQueue.Count > 0)
		{
			// Resource.FindObjectsOfTypeAll<GameObject> was giving me objects in invalid states
			var gameObjects = FindObjectsOfType<GameObject>();
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
