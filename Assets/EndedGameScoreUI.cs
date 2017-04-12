using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[AllowedState(StateNames.EndedGame)]
public class EndedGameScoreUI : Pieka
{
	protected override void OnEnterToActiveState ()
	{
		Text text = GetComponent<Text> ();
		text.text = Core.Scores.Score.ToString();
	}
}
