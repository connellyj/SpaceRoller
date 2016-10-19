using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public float speed;
    public Text scoreText;
    public Text winText;

    Rigidbody rb;
    int count;

    void Start() {
        count = 0;
        SetScoreText();
        winText.text = "";
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Collectable")) {
            Destroy(other.gameObject);
            count++;
            SetScoreText();
        }
    }

    void SetScoreText() {
        scoreText.text = "Count: " + count.ToString();
        if(count >= 8) {
            winText.text = "You Win!";
        }
    }
}
