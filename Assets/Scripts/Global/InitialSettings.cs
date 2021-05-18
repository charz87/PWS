using UnityEngine;
using System.Collections;

public class InitialSettings : MonoBehaviour
{	
	public Texture[] languageTextures;
	public GUITexture[] languageText;
	
	public GameObject turnOn;
	public GameObject turnOff;
	
	public LanguageMesh meshTextObjects;
	public LanguageGuiTexture guiTextureObjects;
	
	public bool texturesOnLoad;
	
	void Start()
	{
		if(texturesOnLoad)
		{
			setTexturesToLanguage();
		}
	}
	public void setTexturesToLanguage ()
	{
		if(meshTextObjects)
			meshTextObjects.setTextures();
		if(guiTextureObjects)
			guiTextureObjects.setTextures();
		if(turnOff.activeSelf == true)
		{
			//GetComponent<GameSerializer>().loadGame();
			//SaveLoad.Load();
			turnOn.SetActive(true);
			turnOff.SetActive(false);
		}
	}
	public void setLanguage()
	{
		if (Game.current.generalStats.currentLanguage == 1)
			{	
				Game.current.generalStats.currentLanguage = 0;
				Debug.Log(Game.current.generalStats.currentLanguage);
			}
		else
			{
				Game.current.generalStats.currentLanguage = 1;
				Debug.Log (Game.current.generalStats.currentLanguage);
			}
		for(int i = 0; i<2; i++)
		{
			languageText[i].texture = languageTextures[Game.current.generalStats.currentLanguage];
		}
	}
}
