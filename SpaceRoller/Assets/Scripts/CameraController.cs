﻿/* Controls the camera based on user input
 */

using UnityEngine;

public class CameraController : MonoBehaviour {

    static readonly Vector3 initialOffset = new Vector3(0, 5.3f, -10.8f);
    static readonly Quaternion rotationOffset = Quaternion.Euler(27.6f, 0, 0);

    float speed;
    bool cameraInitialized = false;
    GameObject player;
    Vector3 offset;
    Vector3 initialCameraPosition;
    Quaternion initialCameraRotation;

    void LateUpdate() {
        if(cameraInitialized) {
            UpdateCameraPosition();
            UpdateCameraRotation();
            UpdateOffset();
        }
    }

    // Passes necessary information to the camera and initializes starting variables
    public void InitCamera(GameObject thePlayer, float speed) {
        this.speed = speed;
        player = thePlayer;
        offset = initialOffset;
        UpdateCameraPosition();
        transform.rotation = rotationOffset;
        cameraInitialized = true;
    }

    // Moves the camera with the player
    void UpdateCameraPosition() {
        transform.position = player.transform.position + offset;
    }

    // Rotates the camera based on player input
    void UpdateCameraRotation() {
        float h = 0f;
        float v = 0f;
        float xRotationLimit = Mathf.Abs(Mathf.Acos(Vector3.Dot(transform.up, Vector3.up))) % (Mathf.PI / 2);

        // Gets the input
        if(Input.GetKey(KeyCode.D)) h = -speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.A)) h = speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.W) && xRotationLimit < Mathf.PI / 3) v = speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.S) && xRotationLimit > 0.1f) v = -speed * Time.deltaTime;

        // Rotates the camera
        if(h != 0f) transform.RotateAround(player.transform.position, Vector3.up, h);
        if(v != 0f) transform.RotateAround(player.transform.position, transform.right, v);
    }

    // Returns the vector between the player and camera positions
    void UpdateOffset() {
        offset = transform.position - player.transform.position;
    }
}