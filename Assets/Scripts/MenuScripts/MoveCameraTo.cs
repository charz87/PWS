using UnityEngine;
using System.Collections;

public class MoveCameraTo : MonoBehaviour
{
	public GameObject cameraRotate;
	public GameObject cameraMove;
	public Transform target;
	public float moveTime;
	public float delay;

	void OnMouseDown ()
	{
		iTween.MoveTo(cameraMove, iTween.Hash("position", target.position, "time", moveTime, "delay", delay));
		iTween.RotateTo(cameraRotate, iTween.Hash("rotation", target, "time", moveTime, "delay", delay));
	}
}
