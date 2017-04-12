using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugText : Pieka
{
	[OnEvent(EventIDs.Lifecycle.StateChange)]	
	void OnStateChange(string id, PMEventArgs args )
	{
		string stateName = args.Text;

		Text text = GetComponent<Text> ();
		text.text = "State name: " + stateName;
		

	}

}
