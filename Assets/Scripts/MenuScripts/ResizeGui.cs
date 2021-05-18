using UnityEngine;
using System.Collections;

public class ResizeGui : MonoBehaviour
{
	public Vector2 screenScale;
	public Vector2 workSpaceDimensions;
	public Vector2 desiredDimensions;

	void Start ()
	{
		float myScreenWidth = (Screen.width * desiredDimensions.x / workSpaceDimensions.x) * screenScale.x;
		float myScreenHeight = (Screen.height * desiredDimensions.y / workSpaceDimensions.y) * screenScale.y;
		float deviceAspectRatioConvert = (workSpaceDimensions.x / workSpaceDimensions.y) / ((float)Screen.width / (float)Screen.height);
		myScreenHeight = myScreenHeight / deviceAspectRatioConvert;
		guiTexture.pixelInset = new Rect(-myScreenWidth/2, -myScreenHeight/2, myScreenWidth, myScreenHeight);
	}
}
