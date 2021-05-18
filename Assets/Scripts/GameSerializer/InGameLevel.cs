using UnityEngine;
using System.Collections;

public class InGameLevel : MonoBehaviour
{
	public int worldID;
	public int levelID;
	public bool available;
	public bool cleared;
	public int localScore;
	public float bestTime;
	public float accuracy;

	public string levelToLoad;
	
	void OnMouseDown ()
	{
		if(available)
		{
			if(GameManager.fromGameToWorkshop){

				GameManager.fromGameToWorkshop=false;
				if(!PlayerPrefs.HasKey("playedOnce"))
					Application.LoadLevel("5_Comic");
				else
					Application.LoadLevel(levelToLoad);

			}

			else{

				GameManager.currentLevel = levelID - 1;
				if(!PlayerPrefs.HasKey("playedOnce"))
					Application.LoadLevel("5_Comic");
				else
					Application.LoadLevel(levelToLoad);

			}
		}
	}
}
