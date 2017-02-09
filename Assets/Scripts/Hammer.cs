using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Hammer : MonoBehaviour,
	Receiver<Message.ControlActionA>,
	Receiver<Message.ControlActionB>,
	Receiver<Message.GroundCollision>
{

	// Use this for initialization
	void Start ()
	{
		transform.parent = parentObject.transform;
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

	public void receive ( ControlActionA o, GameObject sender )
	{
		Debug.Log( "Hammer charging" );
		charging = true;
	}

	public void receive ( ControlActionB o, GameObject sender )
	{
		Debug.Log( "Thrown with power " + throwPower );

		rbody.isKinematic = false;
		rbody.AddRelativeForce( 0, 0, throwPower, ForceMode.Impulse );
		rbody.AddRelativeTorque( throwPower, 0, 0, ForceMode.Impulse );

		throwPower = 0.0f;
		charging = false;
		transform.parent = null;
	}

	public void receive ( GroundCollision o, GameObject sender )
	{
		// throw new NotImplementedException();
	}

	void OnTriggerEnter ( Collider collider )
	{
		if ( transform.parent == null)
		{
			gameObject.sendMessage( new Message.WeaponCollision() { hitObject = collider.gameObject } );
		}
	}

	private void GroundCollision()
	{
		charging = false;
	}



	public GameObject parentObject;
	public float chargeRate = 10.0f;
	public float maxThrow = 100.0f;

	private bool charging = false;
	private float throwPower = 0.0f;
	private Rigidbody rbody = null;

}
