using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject hammerPrefab;
    public Transform hammerSpawn;

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Throw();
        }  
    }

    void Throw()
    {
        // Create the Bullet from the Bullet Prefab
        var hammer = (GameObject)Instantiate(
            hammerPrefab,
            hammerSpawn.position,
            hammerSpawn.rotation);

        // Add velocity to the hammer
        hammer.GetComponent<Rigidbody>().velocity = hammer.transform.forward * 6;

        // Destroy the hammer after 5 seconds
        Destroy(hammer, 5.0f);
    }
}
