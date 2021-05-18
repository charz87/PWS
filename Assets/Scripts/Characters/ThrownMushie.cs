using UnityEngine;
using System.Collections;

public class ThrownMushie : MonoBehaviour
{
	public GameObject mushieCreated;

	// Use this for initialization
	void OnCollisionEnter (Collision vCollision)
	{
		if(vCollision.collider.tag == "Environment")
		{
			Instantiate(mushieCreated, vCollision.contacts[0].point, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
