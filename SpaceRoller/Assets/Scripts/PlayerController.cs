using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float deathHeight;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        // If the player is low enough, ends the game
        if (transform.position.y < deathHeight) GameManager.OnPlayerDeath();
    }

    void FixedUpdate() {
        // Gets the camera's front and right vectors parallel to the xz-plane
        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraForward = Quaternion.AngleAxis(Camera.main.transform.rotation.x * -180, cameraRight) * Camera.main.transform.forward;

        // Calculates the movement vector
        Vector3 moveHorizontal = Input.GetAxis("Horizontal") * cameraRight;
        Vector3 moveVertical = cameraForward * Input.GetAxis("Vertical");
        Vector3 movement = moveVertical + moveHorizontal;

        // Moves the player

        rb.AddForce(movement * speed);
    }

}
