using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Receiver
{
	void receive ( System.Type t, object o );
}

public class Messenger : MonoBehaviour {

	private HashSet<Receiver> subscribers;

	// Use this for initialization
	void Start () {
		subscribers = new HashSet<Receiver>();
	}

	public void addReceiver( Receiver subscriber )
	{
		subscribers.Add( subscriber );
	}

	public void removeReceiver ( Receiver subscriber )
	{
		subscribers.Remove( subscriber );
	}

	public void sendMessage<T> ( T message )
	{
		foreach ( var receiver in gameObject.GetComponents<Receiver>() )
		{
			receiver.receive( typeof( T ), message );
		}
		
		foreach (var subscriber in subscribers)
		{
			subscriber.receive( typeof( T ), message );
		}

		return;
	}
}
