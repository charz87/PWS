using UnityEngine;
using System.Collections;

public class CustomNextRound : MonoBehaviour
{
	public GameObject objToInteract;
	public LevelSerializer mySerializer;

	void OnMouseDown ()
	{
		StartCoroutine("nextRound");
	}
	
	// Update is called once per frame
	IEnumerator nextRound()
	{
		mySerializer.StartCoroutine("setNextLevel");
		yield return new WaitForSeconds(.1f);
		objToInteract.SetActive(false);
	}
}
