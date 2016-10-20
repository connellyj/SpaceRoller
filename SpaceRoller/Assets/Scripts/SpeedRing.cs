using UnityEngine;
using System.Collections;

public class SpeedRing : MonoBehaviour {

	public float strength;
	public Vector3 direction;

	void Start()
	{
		direction = direction.normalized;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(direction * strength);
		}
	}

}
