using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //The target player
    public Transform player;
    //At what distance will the enemy walk towards the player?
//  public float walkingDistance = 10.0f;
    //In what time will the enemy complete the journey between its position and the players position
    public float smoothTime = 10.0f;
    //Vector3 used to store the velocity of the enemy
    private Vector3 smoothVelocity = Vector3.zero;
    
    //Call every frame
    void Update()
    {
        //Look at the player
        transform.LookAt(player);
		//Calculate distance between player
		//      float distance = Vector3.Distance(transform.position, player.position);
		//If the distance is smaller than the walkingDistance
		//      if (distance < walkingDistance)
		//      {
		//Move the enemy towards the player with smoothdamp

		// take players current position plus a little bit in the direction from bad guy to player to get a point slighlty behind the player to go for
		Vector3 slightlyBehindPlayerPos = player.position + 2 * Vector3.Normalize( player.position - transform.position );

		transform.position = Vector3.SmoothDamp( transform.position, slightlyBehindPlayerPos, ref smoothVelocity, smoothTime );
    }

}