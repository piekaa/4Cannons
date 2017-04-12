using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AllowedState(StateNames.Gameplay)]
public class UIScoreDisplay : PiekaUI
{

	Text text;

	void Awake()
	{
		text = GetComponent < Text > ();
	}

	protected override void OnEnterToActiveState ()
	{
		base.OnEnterToActiveState ();
		text.text = "0";
	}

	protected override void OnExitFromActiveState ()
	{
		base.OnExitFromActiveState ();
	}

	[OnEvent(EventIDs.Time.Tick)]
	void OnTick()
	{ 
		text.text = (Core.Scores.Score).ToString();
	}
}

