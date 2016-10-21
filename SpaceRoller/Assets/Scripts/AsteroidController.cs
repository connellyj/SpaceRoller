using UnityEngine;

public class AsteroidController : MonoBehaviour {

	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player") GameManager.OnPlayerDeath(true);
    }
}
