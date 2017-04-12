using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AllowedState( StateNames.Menu.ScoresLoaded) ]
[AllowedState( StateNames.Menu.LoggedIn) ]
[AllowedState( StateNames.Menu.MainManu) ]
public class MenuBestScoresPanel : PiekaUI 
{

	public MenuBestScores MenuBestScores;


	protected override void OnEnterToActiveStateUI ()
	{
		//MenuBestScores.LoadContent ();
	}

}
