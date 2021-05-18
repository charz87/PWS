using UnityEngine;
using System.Collections;

public class HP_Boost : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		StartCoroutine("lifeTime");
	}
	
	// Update is called once per frame
	public IEnumerator lifeTime ()
	{
		yield return new WaitForSeconds(4);
		Destroy(gameObject);
	}
	
	void OnMouseDown()
	{
		GameManager.hitPoints = Mathf.Clamp (GameManager.hitPoints + 1, 0, 10);
		GameManager myGameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
		myGameManager.updateGUI();
		Destroy(gameObject);
	}
}
