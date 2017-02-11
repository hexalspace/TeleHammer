using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeControl: MonoBehaviour
{

	private static readonly string AXIS_HORIZONTAL = "Horizontal";
	private static readonly string AXIS_VERTICAL = "Vertical";

	public float speed = 2.0f;

	private void Start ()
	{
		//Cursor.lockState = CursorLockMode.Locked;
	}

	void Update ()
	{
		float forwardBackTranslate = Input.GetAxis( AXIS_VERTICAL ) * speed * Time.deltaTime;
		float leftRightTranslate = Input.GetAxis( AXIS_HORIZONTAL ) * speed * Time.deltaTime;

		transform.Translate( leftRightTranslate, 0, forwardBackTranslate );

		//if (Input.GetKeyDown(KeyCode.Escape))
		//{
		//	Cursor.lockState = CursorLockMode.None;
		//}
	}
}
