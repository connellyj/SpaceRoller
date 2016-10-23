using UnityEngine;
using System.Collections;

public class SpeedRing : MonoBehaviour {

	public float strength;
	public Vector3 normal;

	void Start()
	{
		MeshFilter filter = GetComponent<MeshFilter>();
  		normal = filter.transform.TransformDirection(filter.mesh.normals[40]); 
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Vector3 distance = transform.position - other.gameObject.transform.position;
			Vector3 direction = normal;
			if (Vector3.Dot(distance, normal) < 0)
			{
				direction *= -1;
			}


			Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(direction * strength);
		}
	}

}
