using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AllowedState( StateNames.BeforeStart)]
public class UIGameplayStartButton : PiekaUI 
{
	public void OnClick()
	{
		FireEvent (EventIDs.GameplayUI.StartButton);
	}
}
