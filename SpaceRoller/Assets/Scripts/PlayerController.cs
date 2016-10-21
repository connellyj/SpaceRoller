/* Handles player movement
 */

using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float maxVelocity;
    public float deathHeight;

    Rigidbody rb;
    bool dead;

    void Start() {
        rb = GetComponent<Rigidbody>();
        dead = false;
    }

    void Update() {
        CheckIfPlayerDied();
    }

    void FixedUpdate() {
        Move();
    }

    // If the player is low enough, ends the game
    void CheckIfPlayerDied() {
        if(transform.position.y < deathHeight && !dead) {
            GameManager.OnPlayerDeath(false);
            dead = true;
        }
    }

    // Moves the player based on input
    void Move() {
        // Gets the camera's front and right vectors parallel to the xz-plane
        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        // Calculates the movement vector
        Vector3 moveHorizontal = Input.GetAxis("Horizontal") * cameraRight;
        Vector3 moveVertical = cameraForward * Input.GetAxis("Vertical");
        Vector3 movement = moveVertical + moveHorizontal;

        // Moves the player
        if(rb.velocity.magnitude < maxVelocity) rb.AddForce(movement * speed);
    }

}
