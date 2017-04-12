using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AllowedState(StateNames.Menu.MainManu)]
[AllowedState(StateNames.Menu.LoggedIn)]
[AllowedState(StateNames.Menu.ScoresLoaded)]
public class MenuPlayButton : PiekaUI 
{
	public void OnClick()
	{
		FireEvent (EventIDs.Menu.Play);
	}
}
