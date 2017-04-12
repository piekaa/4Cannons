using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuInfo : Pieka {
 
	long ShowTime;
	long ExpireTime = 5000;

	bool loadingScores;


	Text text;
	void Awake()
	{
		text = GetComponent<Text> ();
	}



	void ShowMessage(string message)
	{ 

		loadingScores = false;
		ShowTime = MyTime.Millis ();
		text.text = message;
	}

	[OnEvent(EventIDs.Menu.Play)]
	void OnEvent()
	{
		text.text = "";
	}

//	[OnEvent(EventIDs.Facebook.TokenExpireIn)]
	void OnToken(string id, PMEventArgs args)
	{
		ShowMessage (args.Text);
	}

	[OnEvent(EventIDs.Facebook.LogInFailed)]
	void OnLoginFail(string id, PMEventArgs args)
	{ 
		ShowMessage ("Login failed: " + args.Text); 
	}


	[OnEvent(EventIDs.Facebook.CheckPermissionsError)]
	void OnPermissionCheckError(string id, PMEventArgs args)
	{ 
		if( args.Number == 0 )
			ShowMessage ("Internet connection problem ;("); 
	}




//	[OnEvent(EventIDs.Facebook.LoadScoreFailed)]
	void OnLoadScoresFailed(string id, PMEventArgs args)
	{ 
		ShowMessage ("Loading scores failed: " + args.Text); 
	}


	[OnEvent(EventIDs.Facebook.LoadedScores)]
	void OnLoadScorea(string id, PMEventArgs args)
	{ 
	ShowMessage ("Scores loaded"); 
	}

	[OnEvent(EventIDs.Menu.Info)]
	void OnInfo(string id, PMEventArgs args)
	{
		ShowMessage (args.Text);
	}


	[OnEvent(EventIDs.Facebook.LoadingScores)]
	void OnLoadingScores(string id, PMEventArgs args)
	{ 
		text.text = "Loading scores";
		loadingScores = true;
	}



	protected override void OnUpdate()
	{
		if (MyTime.Millis() - ShowTime >= ExpireTime && loadingScores == false)
			text.text = "";

		if (loadingScores)
		{
			text.text = "Loading scores";

			for(int i = 0 ; i  < (MyTime.Millis() / 200 )%4 ;i++)
				text.text+= ".";
		}

	}

}
