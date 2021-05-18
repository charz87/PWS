using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour
{
	public float speed;
	public Vector3 rotateAxis;
	
	void Update ()
	{
		transform.Rotate(new Vector3(speed * Time.deltaTime * rotateAxis.x, speed * Time.deltaTime * rotateAxis.y, speed * Time.deltaTime * rotateAxis.z));
	}
}
