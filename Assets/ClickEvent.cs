using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : Pieka {

	protected override void OnUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			FireEvent (EventIDs.MouseClick);
		}
	}
}
