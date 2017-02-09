using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Thor : MonoBehaviour, 
	Receiver<Message.ControlForward>,
	Receiver<Message.ControlBackward>,
	Receiver<Message.ControlLeft>,
	Receiver<Message.ControlRight>
{

	// Use this for initialization
	void Start ()
	{
		characterController = gameObject.AddOrGetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	public void receive ( ControlForward o, GameObject sender )
	{
		characterController.Move( transform.forward * velocity * Time.deltaTime );
	}

	public void receive ( ControlBackward o, GameObject sender )
	{
		characterController.Move( -transform.forward * velocity * Time.deltaTime );
	}

	public void receive ( ControlLeft o, GameObject sender )
	{
		characterController.Move( -transform.right * velocity * Time.deltaTime );
	}

	public void receive ( ControlRight o, GameObject sender )
	{
		characterController.Move( transform.right * velocity * Time.deltaTime );
	}

	public float velocity = 1.0f;

	private CharacterController characterController;
}
