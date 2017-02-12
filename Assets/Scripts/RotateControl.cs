using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateControl : MonoBehaviour
{
	private static readonly string AXIS_HORIZONTAL = "Mouse X";
	private static readonly string AXIS_VERTICAL = "Mouse Y";


	public float sensitivity = 10.0f;

	private Vector2 mouseLook;
	private GameObject character;


	// Use this for initialization
	void Start ()
	{
		character = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var mouseDelta = new Vector2( Input.GetAxisRaw( AXIS_HORIZONTAL ), Input.GetAxisRaw( AXIS_VERTICAL ) );
		mouseLook += mouseDelta;

		mouseLook.y = Mathf.Clamp( mouseLook.y, -90.0f, 90.0f );

		transform.localRotation = Quaternion.AngleAxis( -mouseLook.y, Vector3.right );
		character.transform.localRotation = Quaternion.AngleAxis( mouseLook.x, character.transform.up );

	}
}
