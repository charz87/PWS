using UnityEngine;
using System.Collections;

public class SpiderWebTrap : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	public IEnumerator timedOutDestroy (float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		Destroy(gameObject);
	}
}
