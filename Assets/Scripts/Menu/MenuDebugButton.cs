using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDebugButton : Pieka 
{

	public void OnClick()
	{
		FireEvent( EventIDs.Menu.Info, new PMEventArgs("Current State: " + StateController.GetActiveState()) );
	}

 
}
