using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{

	void pauseGame ()
	{
		GameManager.isPaused=true;
		Time.timeScale = 0;
	}
	
	void unPauseGame()
	{
		GameManager.isPaused=false;
		Time.timeScale = 1;
	}
}
