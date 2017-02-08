using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCollider : MonoBehaviour
{
	void OnTriggerEnter ( Collider collider )
	{
		collider.gameObject.sendMessage( new Message.WeaponCollision() );
	}
}
