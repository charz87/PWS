using UnityEngine;
using System.Collections;

public class MenuCicleElement : MonoBehaviour
{
	public Transform targetPosRight;
	public Transform targetPosLeft;
	public float moveTime;
	public GameObject[] buttons;
	
	IEnumerator moveRight ()
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			buttons[i].SetActive(false);
		}
		iTween.MoveTo(gameObject, iTween.Hash("position", targetPosRight, "time", moveTime));
		yield return new WaitForSeconds(moveTime);
		for(int i = 0; i < buttons.Length; i++)
		{
			buttons[i].SetActive(true);
		}
	}
	
	IEnumerator moveLeft ()
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			buttons[i].SetActive(false);
		}
		iTween.MoveTo(gameObject, iTween.Hash("position", targetPosLeft, "time", moveTime));
		yield return new WaitForSeconds(moveTime);
		for(int i = 0; i < buttons.Length; i++)
		{
			buttons[i].SetActive(true);
		}
	}
}
