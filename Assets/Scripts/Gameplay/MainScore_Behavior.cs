using UnityEngine;
using System.Collections;

public class MainScore_Behavior : MonoBehaviour
{
	public TextMesh scoreText;

	// Use this for initialization
	void Start ()
	{
		scoreText.text = "" + Game.current.generalStats.totalScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
