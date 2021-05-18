using UnityEngine;
using System.Collections;

public class TextureOffsetAnimation : MonoBehaviour
{
	public bool xOffset;
	public bool yOffset;
	public float xOffsetSpeed;
	public float yOffsetSpeed;
	
	float xAmount = 0;
	float yAmount = 0;
	// Use this for initialization
	void Start ()
	{
		xOffsetSpeed = -xOffsetSpeed * .1f;
		yOffsetSpeed = -yOffsetSpeed * .1f;
		xAmount = renderer.material.mainTextureOffset.x;
		yAmount = renderer.material.mainTextureOffset.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(xOffset)
		{
			xAmount += xOffsetSpeed * Time.deltaTime;
			renderer.material.mainTextureOffset = new Vector2(xAmount, renderer.material.mainTextureOffset.y);
		}
		if(yOffset)
		{
			yAmount += yOffsetSpeed * Time.deltaTime;
			renderer.material.mainTextureOffset = new Vector2(renderer.material.mainTextureOffset.x, yAmount);
		}
	}
}
