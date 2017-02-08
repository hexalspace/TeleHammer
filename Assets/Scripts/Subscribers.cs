using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscribers : MonoBehaviour
{
	void Start ()
	{
		foreach ( var subscriber in subscribers )
		{
			gameObject.addSubscriber( subscriber );
		}
	}

	public GameObject[] subscribers;
}
