using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBasedHealthComponent : MonoBehaviour, HealthComponent
{
	public float lifeTime = 10.0f;
	public bool isAlive ()
	{
		return lifeTime > Time.realtimeSinceStartup;
	}
}
