using System;
using System.Collections;
using System.Collections.Generic;
using Message;
using UnityEngine;

public class Hammer : MonoBehaviour,
	Receiver<Message.ChargeHammer>,
	Receiver<Message.ThrowHammer>,
	Receiver<Message.HammerAttack>,
	Receiver<Message.EnemyKilledByHammer>,
	Receiver<Message.GroundCollision>
{

	enum HammerState
	{
		HELD,
		CHARGING,
		TRANSITIONING_TO_HELD,
		THROWN
	}

	public float hammerShake = 0.5f;
	public float initialThrowPower = 20.0f;
	public float chargeRate = 10.0f;
	public float maxThrow = 100.0f;
	public float hammerPickupTime = 1.0f;
	public GameObject hammerOwner;
	public AudioClip throwSound;
	public AudioClip chargeSound;
	public AudioClip pickupSound;
	public AudioClip groundHitSound;
	public AudioClip enemyHitSound;



	private float elapsedHammerPickupTime = 0.0f;
	private Vector3 droppedLocalPosition;
	private Quaternion droppedLocalRotation;

	private HammerState hammerState = HammerState.HELD;
	private float throwPower = 0.0f;
	private Rigidbody rbody = null;
	private AudioSource regularSource;
	private AudioSource loopSource;

	private Vector3 initialLocalPosition;
	private Quaternion initialLocalRotation;
	private Transform initialParentTransform;

	void Start ()
	{
		initialParentTransform = gameObject.transform.parent;
		initialLocalPosition = transform.localPosition;
		initialLocalRotation = transform.localRotation;

		rbody = gameObject.AddOrGetComponent<Rigidbody>();
		rbody.isKinematic = true;

		regularSource = gameObject.AddComponent<AudioSource>();
		loopSource = gameObject.AddComponent<AudioSource>();
		loopSource.loop = true;
		loopSource.clip = chargeSound;

		throwPower = initialThrowPower;
	}
	
	void Update ()
	{
		if ( hammerState == HammerState.CHARGING )
		{
			throwPower += chargeRate * Time.deltaTime;
			Debug.Log( throwPower );

			if ( throwPower > maxThrow )
			{
				throwPower = maxThrow;
			}

			if (!loopSource.isPlaying)
			{
				loopSource.Play();
			}

			float percentage = throwPower / maxThrow;

			transform.localPosition = initialLocalPosition;
			float newXTransform = transform.localPosition.x + UnityEngine.Random.Range( 0, percentage * hammerShake );
			Vector3 shakenTransform = transform.localPosition;
			shakenTransform.x = newXTransform;
			transform.localPosition = shakenTransform;

			loopSource.volume = ( percentage );
		}
		else if (hammerState == HammerState.TRANSITIONING_TO_HELD)
		{
			loopSource.Stop();
			elapsedHammerPickupTime += Time.deltaTime;
			float percentagePickupTime = Mathf.Clamp01(elapsedHammerPickupTime / hammerPickupTime);

			transform.localPosition = Vector3.Lerp( droppedLocalPosition, initialLocalPosition, percentagePickupTime );
			transform.localRotation = Quaternion.Lerp( droppedLocalRotation, initialLocalRotation, percentagePickupTime );

			if ( elapsedHammerPickupTime >= hammerPickupTime )
			{
				rbody.isKinematic = true;
				hammerState = HammerState.HELD;
			}
		}
		else
		{
			// State management sucks in Unity!
			loopSource.Stop();
		}
	}

	public void receive ( ChargeHammer o, GameObject sender )
	{
		if (hammerState != HammerState.HELD)
		{
			return;
		}

		Debug.Log( "Hammer charging" );
		hammerState = HammerState.CHARGING;
	}

	public void receive ( ThrowHammer o, GameObject sender )
	{
		if ( hammerState != HammerState.CHARGING )
		{
			return;
		}

		Debug.Log( "Thrown with power " + throwPower );

		rbody.isKinematic = false;
		rbody.AddRelativeForce( 0, 0, throwPower, ForceMode.Impulse );
		rbody.AddRelativeTorque( throwPower, 0, 0, ForceMode.Impulse );

		regularSource.PlayOneShot( throwSound,  Mathf.Clamp01(throwPower/maxThrow));

		throwPower = initialThrowPower;
		hammerState = HammerState.THROWN;
		transform.parent = null;


	}

	public void TransitionToHeld()
	{
		regularSource.PlayOneShot( pickupSound );
		rbody.isKinematic = true;
		transform.parent = initialParentTransform;
		elapsedHammerPickupTime = 0.0f;
		droppedLocalPosition = transform.localPosition;
		droppedLocalRotation = transform.localRotation;
		hammerState = HammerState.TRANSITIONING_TO_HELD;
	}

	public void receive ( HammerAttack o, GameObject sender )
	{
		if (sender != gameObject || o.hitObject != hammerOwner || hammerState != HammerState.THROWN)
		{
			return;
		}

		TransitionToHeld();
	}

	public void receive ( EnemyKilledByHammer o, GameObject sender )
	{
		regularSource.PlayOneShot( enemyHitSound );
		if (hammerState == HammerState.THROWN)
		{
			TransitionToHeld();
		}
	}

	public void receive ( GroundCollision o, GameObject sender )
	{
		if ( o.hitObject == gameObject )
		{
			regularSource.PlayOneShot( groundHitSound );
		}
	}

	void OnCollisionEnter( Collision collision)
	{
		if ( hammerState == HammerState.THROWN )
		{
			gameObject.sendMessage( new Message.HammerAttack() { hitObject = collision.gameObject } );
		}
	}

	void OnTriggerEnter ( Collider collider )
	{
		if (hammerState == HammerState.THROWN)
		{
			gameObject.sendMessage( new Message.HammerAttack() { hitObject = collider.gameObject } );
		}
	}


}
