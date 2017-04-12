using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AllowedState(StateNames.Menu.MainManu)]
public class LogInFacebookButton : PiekaUI 
{
	public void OnClick()
	{
		FacebookManager fb = FacebookManager.GetInstance ();
		fb.LogIn ();
	}
}
