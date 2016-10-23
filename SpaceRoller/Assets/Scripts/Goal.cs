/* Controls the goal objects
 */

using UnityEngine;

public class Goal : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") GameManager.OnPlayerVictory();
    }
}
