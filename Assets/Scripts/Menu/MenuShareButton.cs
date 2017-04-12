using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AllowedState(StateNames.Menu.LoggedIn)]
[AllowedState(StateNames.Menu.ScoresLoaded)]

public class MenuShareButton : PiekaUI 
{
	public void OnClick()
	{
		FacebookManager.GetInstance ().Share ();	
	}
}
