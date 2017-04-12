using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[AllowedState(StateNames.Menu.LoggedIn)]
[AllowedState(StateNames.Menu.MainManu)]
[AllowedState(StateNames.Menu.ScoresLoaded)]
public class ShowOnlyInWebGl : PiekaUI {

	protected override void OnEnterToActiveStateUI ()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer )
		{
			gameObject.SetActive (false);
			return;
		}	
	}
}
