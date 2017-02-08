using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
	private static int LEFT_MOUSE_BUTTON = 0;

	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown( LEFT_MOUSE_BUTTON ) )
		{
			gameObject.sendMessage( new Message.ControlActionA() );
		}

		if ( Input.GetMouseButtonUp( LEFT_MOUSE_BUTTON ) )
		{
			gameObject.sendMessage( new Message.ControlActionB() );
		}
	}
}
