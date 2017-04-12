using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;
public class UIMenuController : Pieka 
{


	[OnEvent( EventIDs.Lifecycle.FirstUpdate) ]
	void FirstUpdate()
	{ 
//		print ("First update");
		if (FB.IsLoggedIn)
		{
			StateController.SetActiveState (StateNames.Menu.LoggedIn);
			FireEvent (EventIDs.Facebook.LoggedIn);
		} else
		{
			StateController.SetActiveState (StateNames.Menu.MainManu);
		}
	}


	[OnEvent( EventIDs.Facebook.FacebookInitComplete) ]
	void OnFBInit()
	{
		FacebookManager fbm = FacebookManager.GetInstance ();
		if (FB.IsLoggedIn )
		{
			StateController.SetActiveState (StateNames.Menu.LoggedIn);
			FireEvent (EventIDs.Facebook.LoggedIn);
		}
	}



	[OnEvent(EventIDs.Menu.Play)]
	void OnPlayButton()
	{
		StateController.SetActiveState (StateNames.Menu.Loading);
		FacebookDataStore.SaveToFile ();
		SceneManager.LoadScene (SceneIds.Gameplay);
	}


	[OnEvent(EventIDs.Facebook.LoggedIn)]
	void OnLoggedIn()
	{
		StateController.SetActiveState (StateNames.Menu.LoggedIn);
		FacebookManager.GetInstance ().LoadScore ();
	}


	[OnEvent(EventIDs.Facebook.LoadedScores)]
	void OnLoadedScores()
	{
		StateController.SetActiveState (StateNames.Menu.ScoresLoaded);
	}


	[OnEvent(EventIDs.Facebook.ScoresSaved) ]
	void OnSavedScores()
	{
		FacebookManager.GetInstance ().LoadScore ();
	}

 
	protected override void OnUpdate()
	{
		if(Input.GetKey(KeyCode.Escape))
			Application.Quit();
	}


	[OnEvent(EventIDs.Facebook.PermissionsNotGranted)] 
	void OnPermissionsNotGranted()
	{
		FireEvent(EventIDs.Menu.Info, new PMEventArgs( "Not all permissions granted, Log In Again"));
		FacebookManager.GetInstance ().LogOut ();
		StateController.SetActiveState (StateNames.Menu.MainManu);
	}

	[OnEvent(EventIDs.Facebook.LogInFailed)]
	[OnEvent(EventIDs.Facebook.CheckPermissionsError)] 
	void OnPermissionError(string id, PMEventArgs args)
	{

		print (" Permission check error: ");
		print (args.Number);
 

		if (args.Number == 190 )
		{
			print (" In if of error ");

			FireEvent (EventIDs.Menu.Info, new PMEventArgs ("Facebook login problem, Log In Again"));
			FacebookManager.GetInstance ().LogOut ();
			StateController.SetActiveState (StateNames.Menu.MainManu);
		}
	}

	 



}
