/* Controls the asteroid's spin and interaction
*/

using UnityEngine;

public class Asteroid : MonoBehaviour {

	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") GameManager.OnPlayerDeath(true);
    }
}
