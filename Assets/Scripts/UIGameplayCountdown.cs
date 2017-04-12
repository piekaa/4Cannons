using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AllowedState(StateNames.Countdown)]
public class UIGameplayCountdown : PiekaUI 
{

	float time;

	Text text;


	void Awake()
	{
		text = GetComponent<Text> ();
	}

	protected override void OnEnterToActiveStateUI ()
	{
		time = 3;



	}

	[OnEvent(EventIDs.Time.Tick)]
	void OnTick()
	{
		time -= 0.1f; 

		text.text = Mathf.Ceil (time).ToString();



		if (time <= 0)
		{
			FireEvent (EventIDs.Game.CountdownEnd);
		}


	}



}
