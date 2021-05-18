using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
	public AudioClip mindtrickAudio;

	void Start ()
	{
		audio.PlayOneShot(mindtrickAudio);
		StartCoroutine("splashScreen");
	}
	
	IEnumerator splashScreen ()
	{
		yield return new WaitForSeconds(mindtrickAudio.length);
		Application.LoadLevel("1_MenuScene");
	}
}
