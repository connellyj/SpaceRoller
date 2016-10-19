/*
 * Controls the checkpoint objects
 */ 

using UnityEngine;

public class CheckPointController : MonoBehaviour {

    // When the player collides with the checkpoint, change the spawn location
	void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            GameManager.SetSpawnLocation(transform.position);
        }
    }
}
