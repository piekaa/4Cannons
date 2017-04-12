using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class UIGameplayPlayAgainButton : Pieka 
{
	public void OnClick()
	{
		FireEvent (EventIDs.GameplayUI.PlayAgainButton);
	}
}