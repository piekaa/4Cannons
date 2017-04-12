using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiekaController : Pieka {


	void Awake()
	{


		Pieka[] piekas = FindObjectsOfType<Pieka> ();

		firstUpdate = true;
		foreach (Pieka pieka in piekas)
		{
			pieka.Initialize (); 
		}

	}


	public string FirstState;

	public bool SetFirstState;
	bool firstUpdate = true;



	protected override void OnUpdate()
	{
 
		if (firstUpdate)
		{
			firstUpdate = false;

			FireEvent (EventIDs.Lifecycle.FirstUpdate);

		

			if( SetFirstState )
				StateController.SetActiveState (FirstState);
		}
	}


	public static Pieka InstantiatePieka(Pieka Original)
	{
		Pieka newObject =  (Pieka) Instantiate (Original);
		newObject.Initialize ();
		return newObject;
	}

	public static void DestroyPieka(Pieka pieka)
	{
		pieka.Deinitialize ();
	}

}
