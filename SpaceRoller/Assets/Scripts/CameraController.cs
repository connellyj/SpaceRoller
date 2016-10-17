using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float speed;

    Vector3 offset;

    void Start() {
        offset = GetOffset();
    }

    void LateUpdate() {
        transform.position = player.transform.position + offset;
        float h = 0f;
        if(Input.GetKey(KeyCode.X)) h = -speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.Z)) h = speed * Time.deltaTime;
        if(h != 0f) {
            transform.RotateAround(player.transform.position, Vector3.up, h);
            offset = GetOffset();
        }
    }

    Vector3 GetOffset() {
        return transform.position - player.transform.position;
    }
}
