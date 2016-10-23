/* Controls the goal objects
 */

using UnityEngine;

public class Goal : MonoBehaviour {

    public AudioClip victorySound;

	void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            GameManager.OnPlayerVictory();
            AudioSource.PlayClipAtPoint(victorySound, transform.position);
        }
    }
}
