using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
	private static readonly string ACTION_A = "ActionA";

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown( ACTION_A ) )
		{
			gameObject.sendMessage( new Message.ControlActionA() );
		}

		if ( Input.GetButtonUp( ACTION_A ) )
		{
			gameObject.sendMessage( new Message.ControlActionB() );
		}

		if ( Input.GetKey( KeyCode.W ) )
		{
			gameObject.sendMessage( new Message.ControlForward() );
		}

		if (Input.GetKey( KeyCode.A ) )
		{
			gameObject.sendMessage( new Message.ControlLeft() );
		}

		if ( Input.GetKey( KeyCode.S ) )
		{
			gameObject.sendMessage( new Message.ControlBackward() );
		}

		if ( Input.GetKey( KeyCode.D ) )
		{
			gameObject.sendMessage( new Message.ControlRight() );
		}
	}
}
