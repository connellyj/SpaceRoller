/*
 * Controls the player
 */

using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float deathHeight;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        CheckIfPlayerDied();
    }

    void FixedUpdate() {
        if(GameManager.IsPlayerInputAccepted()) Move();
    }

    // If the player is low enough, ends the game
    void CheckIfPlayerDied() {
        if(transform.position.y < deathHeight) {
            GameManager.OnPlayerDeath(false);
        }
    }

    void Move() {
        // Gets the camera's front and right vectors parallel to the xz-plane
        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

        // Calculates the movement vector
        Vector3 moveHorizontal = Input.GetAxis("Horizontal") * cameraRight;
        Vector3 moveVertical = cameraForward * Input.GetAxis("Vertical");
        Vector3 movement = moveVertical + moveHorizontal;

        // Moves the player

        rb.AddForce(movement * speed);
    }

}
