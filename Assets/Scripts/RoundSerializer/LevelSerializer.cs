using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Net;

public class LevelSerializer : MonoBehaviour
{
	public enum idLevel
	{
		LEVEL_1_ROUNDS,
		LEVEL_2_ROUNDS,
		LEVEL_3_ROUNDS,
		LEVEL_4_ROUNDS
    }
    public idLevel level_ID;
    public Invoker myInvoker;
    
	LevelManager myLevelManager;
	bool firstTimeEntry = true;
	
	public IEnumerator setNextLevel()
	{
		if(firstTimeEntry)
		{
			TextAsset textAsset = (TextAsset)Resources.Load("RoundDocument", typeof(TextAsset));
			yield return myLevelManager = LevelManager.Load(textAsset.text);
			firstTimeEntry = false;
		}
		else
		{
			yield return new WaitForSeconds(1);
		}
		switch (level_ID)
		{
		case idLevel.LEVEL_1_ROUNDS:
			myInvoker.maxObjectCount = myLevelManager.level_1_rounds[GameManager.currentLevel].morado + myLevelManager.level_1_rounds[GameManager.currentLevel].rojo +
				myLevelManager.level_1_rounds[GameManager.currentLevel].azul + myLevelManager.level_1_rounds[GameManager.currentLevel].morado_G;
				
			myInvoker.fillEnemiesList(myLevelManager.level_1_rounds[GameManager.currentLevel].morado, myLevelManager.level_1_rounds[GameManager.currentLevel].rojo,
			                          myLevelManager.level_1_rounds[GameManager.currentLevel].azul, myLevelManager.level_1_rounds[GameManager.currentLevel].morado_G);
			                          
			myInvoker.frequency = myLevelManager.level_1_rounds[GameManager.currentLevel].frequency;
			break;
		}
	}
}
