using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float speed;

    Vector3 offset;

    void Start() {
        offset = getOffset();
    }

    void LateUpdate() {
        transform.position = player.transform.position + offset;
        float h = 0f;
        if(Input.GetKey(KeyCode.X)) h = -speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.Z)) h = speed * Time.deltaTime;
        if(h != 0f) {
            transform.RotateAround(player.transform.position, Vector3.up, h);
            offset = getOffset();
        }
    }

    Vector3 getOffset() {
        return transform.position - player.transform.position;
    }
}
