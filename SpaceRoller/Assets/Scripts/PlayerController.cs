using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraForward = Quaternion.AngleAxis(Camera.main.transform.rotation.x * -180, cameraRight) * Camera.main.transform.forward;

        Vector3 moveHorizontal = Input.GetAxis("Horizontal") * cameraRight;
        Vector3 moveVertical = cameraForward * Input.GetAxis("Vertical");

        Vector3 movement = moveVertical + moveHorizontal;

        rb.AddForce(movement * speed);
    }
}
