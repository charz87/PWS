using UnityEngine;
using System.Collections;

public class ResizeGuiText : MonoBehaviour
{
	public float workSpaceWidth;
	public int desiredFontSize;

	void Start ()
	{
		float deviceAspectRatioConvert = Screen.width / workSpaceWidth;
		guiText.fontSize = (int)((float)desiredFontSize * deviceAspectRatioConvert);
	}
}