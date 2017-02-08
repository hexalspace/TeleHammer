using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Receiver
{
	void receive ( System.Type t, object o );
}

public class Messenger : MonoBehaviour {

	private HashSet<GameObject> subscribers;

	// Use this for initialization
	void Start ()
	{
		initialize();
	}

	public void initialize()
	{
		if (initialized)
		{
			return;
		}

		subscribers = new HashSet<GameObject>();
		addSubscriber( gameObject );
		initialized = true;
	}

	public void addSubscriber( GameObject subscriber )
	{
		if (subscriber == null)
		{
			Debug.Log( "addSubscriber passed a null subscriber, this shouldn't happen" );
			return;
		}
		subscribers.Add( subscriber );
	}

	public void removeSubscriber ( GameObject subscriber )
	{
		subscribers.Remove( subscriber );
	}

	public void sendMessage<T> ( T message )
	{
		foreach (var subscriber in subscribers)
		{
			foreach ( var receiver in subscriber.GetComponents<Receiver>() )
			{
				receiver.receive( typeof( T ), message );
			}
		}
	}

	private bool initialized = false;
}
