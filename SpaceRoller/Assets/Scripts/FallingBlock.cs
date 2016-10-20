using UnityEngine;
using System.Collections;

public class FallingBlock : MonoBehaviour {

	public float shakeTime;
	public float shakeSpeed;
	public float fallTime;
	public float fallSpeed;
	
	private bool fallTriggered = false;
	private float framesPerSecond = 30;
	private Vector3 origin;

	void Start() 
	{
		origin = transform.position;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player" && !fallTriggered) 
		{		
			StartCoroutine(Fall());
		}
	}

	IEnumerator Fall() 
	{
		// shake
		fallTriggered = true;
		int currentFrameIndex = 0;
		int direction = 1;
		float shakeFrames = shakeTime * framesPerSecond;
		while (currentFrameIndex < shakeFrames)
		{
	 	   transform.Rotate(Vector3.up * shakeSpeed * direction);
	 	   yield return new WaitForSeconds(1f/framesPerSecond);
	 	   direction *= -1;
	 	   currentFrameIndex++;
		}

		// fall
		currentFrameIndex = 0;
		float fallingFrames = fallTime * framesPerSecond;
        while (currentFrameIndex < fallingFrames)
        {
            transform.position += Vector3.down * fallSpeed;
            yield return new WaitForSeconds(1f/framesPerSecond);
            currentFrameIndex++;
        }

        // return to original position
		transform.position = origin;
		fallTriggered = false;
	}
}
