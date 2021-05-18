using UnityEngine;
using System.Collections;

public class LanguageMesh : MonoBehaviour
{
	public Renderer[] renderedTexts;
	public Texture[] englishTextures;
	public Texture[] spanishTextures;
	
	public bool texturesOnLoad;

	void Start()
	{
		if(texturesOnLoad)
		{
			setTextures();
		}
	}
	
	public void setTextures ()
	{
		if(Game.current.generalStats.currentLanguage == 0)
		{
			for (int i = 0; i<renderedTexts.Length; i++)
			{
				renderedTexts[i].material.mainTexture = englishTextures[i];
			}
		}
		else
		{
			for (int i = 0; i<renderedTexts.Length; i++)
			{
				renderedTexts[i].material.mainTexture = spanishTextures[i];
			}
		}
	}
}
