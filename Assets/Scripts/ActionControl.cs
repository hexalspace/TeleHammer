using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionControl : MonoBehaviour
{
	private static readonly string BUTTON_ACTION_A = "ActionA";

	void Update ()
	{
		if ( Input.GetButtonDown( BUTTON_ACTION_A ) )
		{
			gameObject.sendMessage( new Message.ChargeHammer() );
		}

		if ( Input.GetButtonUp( BUTTON_ACTION_A ) )
		{
			gameObject.sendMessage( new Message.ThrowHammer() );
		}
	}
}
