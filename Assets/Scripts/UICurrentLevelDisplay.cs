using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AllowedState(StateNames.Gameplay)]
[AllowedState(StateNames.EndedGame)]
public class UICurrentLevelDisplay : PiekaUI 
{

	Text text;
	
	public string TextBefore;

	void Awake()
	{
		text = GetComponent<Text> ();
	}

	protected override void OnEnterToActiveStateUI ()
	{
		SetLevel ();
	}

	[OnEvent(EventIDs.Game.Start)]
	[OnEvent(EventIDs.Time.NextLevel)]
	void SetLevel()
	{
		text.text = TextBefore + Data.CurrentLevel; 
	}



}
