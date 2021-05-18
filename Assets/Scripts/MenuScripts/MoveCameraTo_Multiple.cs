using UnityEngine;
using System.Collections;

public class MoveCameraTo_Multiple : MonoBehaviour
{
	public GameObject cameraRotate;
	public GameObject cameraMove;
	public Transform[] targets;
	public float[] moveTimes;
	public float[] delays;

	void OnMouseDown ()
	{
		StartCoroutine("moveRoutine");
	}
	
	public IEnumerator moveRoutine ()
	{
		for(int i = 0; i < targets.Length; i++)
		{
			yield return new WaitForSeconds(delays[i]);
			iTween.MoveTo(cameraMove, iTween.Hash("position", targets[i].position, "time", moveTimes[i]));
			iTween.RotateTo(cameraRotate, iTween.Hash("rotation", targets[i], "time", moveTimes[i]));
		}
	}
}
