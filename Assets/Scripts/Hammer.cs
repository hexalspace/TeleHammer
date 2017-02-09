using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour, Receiver
{

	// Use this for initialization
	void Start ()
	{
		rbody = gameObject.AddOrGetComponent<Rigidbody>();
		rbody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (charging)
		{
			throwPower += chargeRate * Time.deltaTime;
		}

		if (throwPower > maxThrow)
		{
			throwPower = maxThrow;
		}
	}

	public void receive ( object o, Type t, GameObject g )
	{
		if ( t == typeof( Message.ControlActionA ) )
		{
			HammerCharge();
		}
		else if ( t == typeof( Message.ControlActionB ) )
		{
			HammerThrow();
		}
		else if ( t == typeof( Message.GroundCollision ) )
		{
			GroundCollision();
		}
	}

	private void HammerCharge()
	{
		Debug.Log( "Hammer charging" );
		charging = true;
	}

	private void HammerThrow ()
	{
		Debug.Log( "Thrown with power " + throwPower );

		rbody.isKinematic = false;
		rbody.AddRelativeForce( 0, 0, throwPower, ForceMode.Impulse );
		rbody.AddRelativeTorque( throwPower, 0, 0, ForceMode.Impulse );

		throwPower = 0.0f;
		charging = false;
	}

	private void GroundCollision()
	{
		charging = false;
	}

	public float chargeRate = 10.0f;
	public float maxThrow = 100.0f;

	private bool claimed = false;
	private bool charging = false;
	private float throwPower = 0.0f;
	private Rigidbody rbody = null;

}
