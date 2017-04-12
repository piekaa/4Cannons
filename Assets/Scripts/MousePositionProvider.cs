using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionProvider : IPositionProvider 
{
	public Vector2 GetWorldPosition ()
	{
		Vector3 newPos = Input.mousePosition;
		newPos.z = 0;

		newPos = Camera.main.ScreenToWorldPoint (newPos);

		newPos.z = 0;
		return newPos;
	}	
}
