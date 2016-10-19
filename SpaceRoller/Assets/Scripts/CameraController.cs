/*
 * Controls the camera based on user input
 */

using UnityEngine;

public class CameraController : MonoBehaviour {

    float speed;
    bool cameraInitialized = false;
    GameObject player;
    Vector3 offset;

    void LateUpdate() {
        if(cameraInitialized) {
            UpdateCameraPosition();
            UpdateCameraRotation();
            offset = GetOffset();
        }
    }

    // Passes necessary information to the camera
    public void InitCamera(GameObject thePlayer, float speed) {
        this.speed = speed;
        player = thePlayer;
        offset = GetOffset();
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
        float xRotationLimit = Mathf.Abs(Mathf.Acos(Vector3.Dot(Camera.main.transform.up, Vector3.up))) % (Mathf.PI / 2);
        if(Input.GetKey(KeyCode.D)) h = -speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.A)) h = speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.W) && xRotationLimit < 1f) v = speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.S) && xRotationLimit > 0.1f) v = -speed * Time.deltaTime;
        if(h != 0f) transform.RotateAround(player.transform.position, Vector3.up, h);
        if(v != 0f) transform.RotateAround(player.transform.position, Camera.main.transform.right, v);
    }

    // Returns the vector between the player and camera transforms
    Vector3 GetOffset() {
        return transform.position - player.transform.position;
    }
}
