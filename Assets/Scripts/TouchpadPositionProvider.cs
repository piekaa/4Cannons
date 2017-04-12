using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchpadPositionProvider : IPositionProvider {

	public float Speed = 1;

	private Vector3 lastPos;


	private Vector3 targetPos;

	bool firstTime = true;


	bool firstFrame = true;

	public Vector2 GetWorldPosition ()
	{


		if (firstTime)
		{
			firstTime = false;

			targetPos = new Vector2 (Screen.width/2, Screen.height/2);

			targetPos = Camera.main.ScreenToWorldPoint (targetPos);


		}
		Vector3 newPos;
		if (Input.GetMouseButton (0))
		{
			

			if (firstFrame)
			{
				lastPos = Camera.main.ScreenToWorldPoint( Input.mousePosition);
				firstFrame = false;
			} else
			{
				newPos = Camera.main.ScreenToWorldPoint( Input.mousePosition);

				newPos.z = 0;

				targetPos += (newPos - lastPos);

				lastPos = newPos;
			}




		} else
			firstFrame = true;
		 






		return targetPos;


	}



}
