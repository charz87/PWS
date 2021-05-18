using UnityEngine;
using System.Collections;

public class LoadWorkshop : MonoBehaviour {

	public string levelToLoad;
	public Transform desiredPosition;
	public GameObject workShop_Btn;
	
	// Update is called once per frame
	void Start()
	{
		StartCoroutine("checkDistance");
		if(!PlayerPrefs.HasKey("playedOnce"))
		{
			workShop_Btn.SetActive(false);
		}
	}
	public IEnumerator checkDistance ()
	{
		while(true)
		{
			if(Vector3.Distance(transform.position, desiredPosition.position) < .5f)
			{
				Application.LoadLevel(levelToLoad);
			}
			yield return new WaitForSeconds(.5f);
		}
	}
}
