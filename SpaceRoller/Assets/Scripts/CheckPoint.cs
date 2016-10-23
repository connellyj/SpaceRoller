/* Controls the checkpoint objects
 */ 

using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public AudioClip checkpointSound;

    // When the player collides with the checkpoint, change the spawn location
	void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            Vector3 location = transform.position + Vector3.up;
            GameManager.SetSpawnLocation(location);
            AudioSource.PlayClipAtPoint(checkpointSound, location);
        }
    }
}
