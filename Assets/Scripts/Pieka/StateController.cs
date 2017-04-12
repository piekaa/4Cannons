using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateController 
{

	private static List<Pieka> piekas = new List<Pieka> ();

	private static string currentState = StateNames.Countdown; 

	private static ArrayList piekasArray = new ArrayList(); 

	public static void AddPieka(Pieka pieka)
	{
		piekas.Add (pieka);
	}

	public static void RemovePieka(Pieka pieka)
	{
		piekas.Remove (pieka);
	}

	public static void SetActiveState(string name)
	{

		//Debug.Log("Changing state to: " + name);

		SEventSystem.FireEvent (EventIDs.Lifecycle.StateChange, new PMEventArgs (name));
		currentState = name;

		int i = 0;

		foreach (Pieka pieka in piekas)
		{
			if (i >= piekasArray.Count)
				piekasArray.Add (pieka);
			else
				piekasArray [i] = pieka;
			i++;
		}


		for (i--; i >= 0; i--)
		{
			((Pieka)piekasArray [i]).OnStateChange (name);
		}

	}


	public static string GetActiveState()
	{
		return currentState;
	}
}
