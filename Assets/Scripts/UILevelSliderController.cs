using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AllowedState(StateNames.Gameplay)]
public class UILevelSliderController : PiekaUI
{

	Slider slider;

	void Awake()
	{
		slider = GetComponent<Slider> ();
	}


	[OnEvent(EventIDs.Game.Start)]
	void OnStart()
	{
		slider.enabled = true;
	}


	[OnEvent(EventIDs.Time.Tick)]
	void OnTick()
	{
		slider.value = Data.Progress; 
	}





}
