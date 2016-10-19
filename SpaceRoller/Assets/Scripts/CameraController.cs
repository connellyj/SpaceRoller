using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float speed;

    Vector3 offset;

    void Start() {
        offset = GetOffset();
    }

    void LateUpdate() {
        UpdateCameraPosition();
        UpdateCameraRotation();
        offset = GetOffset();
    }

    // Moves the camera with the player
    void UpdateCameraPosition() {
        transform.position = player.transform.position + offset;
    }

    // Rotates the camera based on player input
    void UpdateCameraRotation() {
        float h = 0f;
        if(Input.GetKey(KeyCode.X)) h = -speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.Z)) h = speed * Time.deltaTime;
        if(h != 0f) {
            transform.RotateAround(player.transform.position, Vector3.up, h);
        }
    }

    // Returns the vector between the player and camera transforms
    Vector3 GetOffset() {
        return transform.position - player.transform.position;
    }
}
