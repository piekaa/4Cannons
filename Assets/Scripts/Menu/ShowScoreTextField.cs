using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[AllowedState( StateNames.Menu.ScoresLoaded) ]
[AllowedState( StateNames.Menu.LoggedIn) ]
[AllowedState( StateNames.Menu.MainManu) ]
public class ShowScoreTextField : PiekaUI 
{


	[OnEvent( EventIDs.Menu.RefreshScores) ]
	protected override void OnEnterToActiveStateUI ()
	{ 
		Text text = GetComponent<Text> ();
		text.text = "Top score: " + FacebookManager.GetInstance ().GetFacebookUserProvider ().GetMyScore();

	}




}
