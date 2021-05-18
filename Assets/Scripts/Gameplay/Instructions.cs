using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour
{

	void Start ()
	{
		if(!PlayerPrefs.HasKey("playedOnce"))
		{
			guiTexture.enabled = true;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	void OnMouseDown ()
	{
		Destroy(gameObject);
	}
}
