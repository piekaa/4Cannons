using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class UIGameplayBackToMenuButton : Pieka 
{
	public void OnClick()
	{
		FireEvent (EventIDs.GameplayUI.BackToMenuButton);	
	}
}
