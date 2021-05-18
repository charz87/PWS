using UnityEngine;
using System.Collections;

public class LanguageGuiTexture : MonoBehaviour
{
	public GUITexture[] renderedTexts;
	public Texture[] englishTextures;
	public Texture[] spanishTextures;
	
	public bool texturesOnLoad;
	
	void Start()
	{
		if(texturesOnLoad)
		{
			setTextures();
			Debug.Log (Game.current.generalStats.currentLanguage);
			Debug.Log ("Setting textures");
		}
	}
	
	public void setTextures ()
	{
		if(Game.current.generalStats.currentLanguage == 0)
		{
			for (int i = 0; i<renderedTexts.Length; i++)
			{
				renderedTexts[i].texture = englishTextures[i];
			}
		}
		else
		{
			for (int i = 0; i<renderedTexts.Length; i++)
			{
				renderedTexts[i].texture = spanishTextures[i];
			}
		}
	}
}
