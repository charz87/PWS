using UnityEngine;
using System.Collections;

public class AudioControler : MonoBehaviour {

	public GUITexture audioButton;
	public Texture audioOnTexture;
	public Texture audioOffTexture;

	// Use this for initialization
	void Start () {

		if(GameManager.audioOn==true)
		{
			AudioListener.pause=false;
			audioButton.texture = audioOnTexture;
		} else
		{
			AudioListener.pause=true;
			audioButton.texture = audioOffTexture;
		}
	
	}

	void SwitchAudioState(){

		GameManager.audioOn = !GameManager.audioOn;

		if(GameManager.audioOn==true)
		{
			AudioListener.pause=false;
			audioButton.texture = audioOnTexture;
		} else
		{
			AudioListener.pause=true;
			audioButton.texture = audioOffTexture;
		}

	}

}
