using UnityEngine;
using System.Collections;

public class WorkshopManager : MonoBehaviour {

	public GameObject backToPlayButton;
	public GameObject backToMenuButton;

	// Use this for initialization
	void Start () {

		if(GameManager.fromGameToWorkshop)
		{
			backToPlayButton.SetActive(true);
		}	
		else {

			backToMenuButton.SetActive (true);

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {

		GameManager.fromGameToWorkshop=true;

	}
}
