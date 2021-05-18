using UnityEngine;
using System.Collections;

public class PingPongRotate : MonoBehaviour
{
	public float speed;
	public float amplitude;
	
	float rotateFactor;
	int dir = 1;
	
	void Update ()
	{
		rotateFactor += speed * Time.deltaTime * dir;
		rotateFactor = Mathf.Clamp(rotateFactor, -amplitude * 0.5f, amplitude * 0.5f);
		if(rotateFactor == amplitude * 0.5f || rotateFactor == -amplitude * 0.5f)
			dir = -dir;
		transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, rotateFactor, transform.localRotation.z));
	}
}
