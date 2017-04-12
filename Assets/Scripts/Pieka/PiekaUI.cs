using System.Collections.Generic;
using UnityEngine;
using System.Reflection; 
public class PiekaUI : Pieka
{
	protected virtual void Start()
	{
		gameObject.SetActive (false);
		UnregisterAll ();
	}


	protected Dictionary< MethodInfo, object[] > toInvokeInNextUpdate = new Dictionary< MethodInfo, object[] > ();
	protected Dictionary< MethodInfo, object[] > toInvokeInNextUpdate2 = new Dictionary< MethodInfo, object[] > ();


 

	protected override void OnEnterToActiveState ()
	{
		gameObject.SetActive (true);
		RegisterAll ();
		OnEnterToActiveStateUI ();
	}

	protected override void OnExitFromActiveState ()
	{
		gameObject.SetActive (false);
		UnregisterAll ();
		OnExitFromActiveStateUI ();
	}


	protected virtual void OnEnterToActiveStateUI(){}
	protected virtual void OnExitFromActiveStateUI(){}
}

