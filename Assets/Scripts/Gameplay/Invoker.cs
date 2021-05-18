using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Invoker : MonoBehaviour
{
	public enum kindOfInvoker
	{
		singleObject,
		multiObjectRandom,
		multiObjectFixed
	}
	public kindOfInvoker invokerType;
	
	public enum placesOfInvoking
	{
		singlePlaceStatic,
		singlePlaceDynamic,
		multiPlace
	}
	public placesOfInvoking invokingPlaces;
	//If is singleObject
	public GameObject objectToInvoke;
	//If is multiObject
	public GameObject[] objectsToInvoke;
	public List<int> objectType;
	//if single Origin
	public Transform originPosition;
	//If is multi Origin
	public Transform[] originPositions;
	//maxObjectCount: Used to limit number of instances. unlimited if 0
	public int maxObjectCount;
	int objectCounter;
	//Time between instance creation
	public float frequency;
	//Determines if instances can be created
	public bool active;

	void Start ()
	{
		objectType = new List<int>();
		//StartCoroutine("invoke_Routine");
	}
	
	public void fillEnemiesList(int type1, int type2, int type3, int type4)
	{
		//Debug.Log("Filling Enemy List");
		objectType.Clear();
		for (int i = 0; i<type1; i++)
		{
			objectType.Add(0);
		}
		for (int i = 0; i<type2; i++)
		{
			objectType.Add(1);
		}
		for (int i = 0; i<type3; i++)
		{
			objectType.Add(2);
		}
		for (int i = 0; i<type4; i++)
		{
			objectType.Add(3);
		}
		//Debug.Log(objectType.Count);
	}
	
	public IEnumerator invoke_Routine()
	{
		GameMaster.currentlyPlaying = true;
		GameManager.turretActive = true;
		active = true;
		objectCounter = 0;
		//yield return new WaitForSeconds(1);
		//Debug.Log(GameManager.currentLevel);
		if(GameManager.currentLevel == 30)
		{
			Instantiate(objectsToInvoke[4], originPositions[Random.Range(0, originPositions.Length)].position, Quaternion.identity);
			maxObjectCount = 0;
			frequency = 2;
			maxObjectCount = 40;
			invokerType = kindOfInvoker.multiObjectRandom;
			yield return new WaitForSeconds(2);
		}
		//else
		//{
		switch(invokingPlaces)
		{
		case placesOfInvoking.singlePlaceStatic:
			while(active)
			{
				if (GameManager.mapUnitCount < 8)
				{
					int randObj = Random.Range(0, objectsToInvoke.Length);
					int randType = Random.Range(0, objectType.Count);
					if(maxObjectCount == 0)
					{
						switch(invokerType)
						{
						case kindOfInvoker.singleObject:
							Instantiate (objectToInvoke, originPosition.position, originPosition.rotation);
							break;
						case kindOfInvoker.multiObjectRandom:
							Instantiate (objectsToInvoke[randObj], originPosition.position, originPosition.rotation);
							break;
						case kindOfInvoker.multiObjectFixed:
							Instantiate (objectsToInvoke[objectType[randType]], originPosition.position, originPosition.rotation);
							objectType.Remove(objectType[randType]);
							break;
						}
					}
					else if(objectCounter < maxObjectCount)
					{
						switch(invokerType)
						{
						case kindOfInvoker.singleObject:
							Instantiate (objectToInvoke, originPosition.position, originPosition.rotation);
							objectCounter++;
							break;
						case kindOfInvoker.multiObjectRandom:
							Instantiate (objectsToInvoke[randObj], originPosition.position, originPosition.rotation);
							objectCounter++;
							break;
						case kindOfInvoker.multiObjectFixed:
							Instantiate (objectsToInvoke[objectType[randType]], originPosition.position, originPosition.rotation);
							objectCounter++;
							objectType.Remove(objectType[randType]);
							break;
						}
					}
					else
					{
						active = false;
					}
					yield return new WaitForSeconds(frequency);
				}
				yield return null;
			}
			break;
		case placesOfInvoking.multiPlace:
			while(active)
			{
				if (GameManager.mapUnitCount < 8)
				{
					int randInt = Random.Range(0, originPositions.Length);
					int randObj = Random.Range(0, objectsToInvoke.Length);
					int randType = Random.Range(0, objectType.Count);
					int bossRandom = Random.Range(0, 3);
					if(maxObjectCount == 0)
					{
						switch(invokerType)
						{
						case kindOfInvoker.singleObject:
							Instantiate (objectToInvoke, originPositions[randInt].position, transform.rotation);
							break;
						case kindOfInvoker.multiObjectRandom:
							if(GameManager.bossAlive)
							{
								if(GameManager.epicMode)
								{
									Instantiate (objectsToInvoke[bossRandom], originPositions[randInt].position, transform.rotation);
								}
								else
								{
									Instantiate (objectsToInvoke[0], originPositions[randInt].position, transform.rotation);
								}
							}
							break;
						case kindOfInvoker.multiObjectFixed:
							Instantiate (objectsToInvoke[objectType[randType]], originPositions[randInt].position, transform.rotation);
							objectType.Remove(objectType[randType]);
							break;
						}
					}
					else if(objectCounter < maxObjectCount)
					{
						switch(invokerType)
						{
						case kindOfInvoker.singleObject:
							Instantiate (objectToInvoke, originPositions[randInt].position, transform.rotation);
							objectCounter++;
							break;
						case kindOfInvoker.multiObjectRandom:
							if(GameManager.bossAlive)
							{
								if(GameManager.epicMode)
								{
									Instantiate (objectsToInvoke[bossRandom], originPositions[randInt].position, transform.rotation);
								}
								else
								{
									Instantiate (objectsToInvoke[0], originPositions[randInt].position, transform.rotation);
								}
							}
							objectCounter++;
							break;
						case kindOfInvoker.multiObjectFixed:
							Instantiate (objectsToInvoke[objectType[randType]], originPositions[randInt].position, transform.rotation);
							objectCounter++;
							objectType.Remove(objectType[randType]);
							break;
						}
					}
					else
					{
						active = false;
					}
					Debug.Log(objectCounter);
					yield return new WaitForSeconds(frequency);
				}
				yield return null;
			}
			break;
		}
		yield return null;
		//}
	}
}