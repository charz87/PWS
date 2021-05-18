using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour
{
	public bool isLocal;
	public Vector3 pos1;
	public Vector3 pos2;
	public float time;
	public float delay;
	// Use this for initialization
	void Start ()
	{
		move1();
	}
	
	// Update is called once per frame
	void move1()
	{
		iTween.MoveTo(gameObject, iTween.Hash("position", pos1, "time", time, "delay", delay, "islocal", isLocal, "oncomplete", "move2"));
	}
	void move2()
	{
		iTween.MoveTo(gameObject, iTween.Hash("position", pos2, "time", time, "delay", delay, "islocal", isLocal, "oncomplete", "move1"));
	}
}
