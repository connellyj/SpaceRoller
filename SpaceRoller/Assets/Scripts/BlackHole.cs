using UnityEngine;
using System.Collections;
using System;

public class BlackHole : MonoBehaviour {

	public float strength;

	void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 distance = transform.position - other.transform.position; 
            Vector3 direction = distance.normalized;
            float magnitude = strength / (float)Math.Pow(distance.magnitude,2.0);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(direction * magnitude);
        }
    }

}
