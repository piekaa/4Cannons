using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;

public class FacebookManager : Pieka 
{ 
 
	private static FacebookManager instance;
	List<string> perms = new List<string> ()  { "public_profile" , "user_friends", "publish_actions", "user_games_activity"  };
	List<string> readPerms = new List<string> ()  { "user_friends","public_profile" , "user_games_activity"  };
	List<string> publishPerms = new List<string> ()  {"publish_actions"};
	private static FacebookUserProvider FacebookUserProvider;


	public FacebookUserProvider GetFacebookUserProvider()
	{
		return FacebookUserProvider;
	}


	public static FacebookManager GetInstance()
	{
		return instance;
	}


	public void LogOut()
	{
		FB.LogOut ();
	}


	#region Initializing


	void Awake ()
	{
		FacebookUserProvider = GetComponent<FacebookUserProvider> ();
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
			instance = this;

		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
 

	}

	void Start()
	{


		if (Application.platform == RuntimePlatform.WebGLPlayer)
			return;

		if (Data.AppStartActionsHandled == false)
		{
			Data.AppStartActionsHandled = true;
			FacebookDataStore.LoadFromFile ();
			FacebookUserProvider.TryToFindMeAndTop10 (); 
		//	Application.targetFrameRate = 40;
		}
	}


	private void InitCallback ()
	{

		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();

			if (Application.platform == RuntimePlatform.WebGLPlayer)
				LogIn ();


			FireEvent (EventIDs.Facebook.FacebookInitComplete);

			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}


	#endregion






	#region Log In

	public void LogIn()
	{ 

		FB.LogInWithReadPermissions (readPerms, LogInReadCallback );
	}


	int tryToFindErrorCode(IResult result)
	{
		int code = 0;
		if (result.ResultDictionary.ContainsKey ("error"))
		{
			object error = result.ResultDictionary ["error"];

			if (error is Dictionary<string, object>)
			{
				Dictionary<string,object> dic = (Dictionary<string,object>)error;
				if( dic.ContainsKey("code") )
				{
					code = int.Parse( dic["code"].ToString() );
				}
			}


		}
		return code;
	}


	void LogInReadCallback(ILoginResult result)
	{ 

		if (result.Error != null)
		{


			int code = tryToFindErrorCode (result);
			print (result.Error);
			print (result.RawResult);

			 
			PMEventArgs args = new PMEventArgs (code);
			args.Text = result.Error;


			print ("facebook Error: " + result.Error);
			FireEvent (EventIDs.Facebook.LogInFailed , args);
			return;
		}

		if (result.Cancelled)
		{
			print ("facebook Log in cancelled");
			FireEvent (EventIDs.Facebook.LogInFailed , new PMEventArgs("cancelled"));
			return;
		} 
		FB.LogInWithPublishPermissions (publishPerms, LoginCallback );

	}

	void LoginCallback(ILoginResult result)
	{
 

		if (result.Error != null)
		{
			int code = tryToFindErrorCode (result);

	
			PMEventArgs args = new PMEventArgs (code);
			args.Text = result.Error;


			print ("facebook Error: " + result.Error);
			FireEvent (EventIDs.Facebook.LogInFailed ,args);
			return;
		}

		if (result.Cancelled)
		{
			print ("facebook Log in cancelled");
			FireEvent (EventIDs.Facebook.LogInFailed , new PMEventArgs("cancelled"));
			return;
		}

		AccessToken token;
		token = result.AccessToken;
		 
		

		HashSet<string> givenPerms = new HashSet<string>();


		foreach (string p in token.Permissions)
		{
			givenPerms.Add (p);
		}


		foreach (string perm in perms)
		{
			if (givenPerms.Contains (perm) == false)
			{
				print ("Error, permision not given");
				FireEvent (EventIDs.Facebook.LogInFailed , new PMEventArgs("not all perms given"));
				return;
			}
		}
		FacebookDataStore.MeInfo.MyId = token.UserId;
		FacebookDataStore.MeInfo.HasLoggedIn = true;
		FacebookDataStore.MeInfo.TokenString = token.TokenString;

		FireEvent (EventIDs.Facebook.LoggedIn);

	}


	#endregion


	#region Check Permissions

	public void CheckPermissions()
	{
//		print (FacebookDataStore.MeInfo.MyId);
		string url = "/" + FacebookDataStore.MeInfo.MyId + "/permissions";

	//	print (url);

		FB.API( url, HttpMethod.GET,  PermissionsCheckResult );
	}

	void PermissionsCheckResult(IGraphResult result)
	{
		
		if (result.Error != null)
		{
			print (result.Error);

			print (result.RawResult);

			int code = tryToFindErrorCode (result);

		
			PMEventArgs args = new PMEventArgs (code);
			args.Text = result.Error;

			print ("facebook Error: " + result.Error);
			FireEvent (EventIDs.Facebook.CheckPermissionsError , args);
			return;
		}

		if (result.Cancelled)
		{
			print ("facebook Log in cancelled");
			FireEvent (EventIDs.Facebook.CheckPermissionsError , new PMEventArgs("cancelled"));
			return;
		}

		Permissions permissions = new Permissions (result.ResultDictionary);


		if (permissions.HasAllPermissions (perms) == false )
		{
			FireEvent (EventIDs.Facebook.PermissionsNotGranted);
		}


	}

	#endregion



	#region Score


	public void SaveScore(long Score)
	{

		CheckPermissions ();

		if ( FB.IsLoggedIn == false)
			return;

		Dictionary<string, string> scoreData = new Dictionary<string, string> ();
		scoreData.Add ("score", Score.ToString ());

		FacebookDataStore.MeInfo.Score = Score;

 
			FB.API ("/" + FacebookDataStore.MeInfo.MyId+ "/scores", HttpMethod.POST, SaveScoreResult, scoreData);
	}

 
	public void SaveScore(string Score)
	{  
		SaveScore (long.Parse (Score));
	}

	void SaveScoreResult(IGraphResult result)
	{
		if (result.Cancelled)
		{
			print ("Save score cancelled ");
		}
		if (result.Error != null)
		{
			print ("Save score error: " + result.Error);
		} 

		FacebookDataStore.MeInfo.HighScoreSavedToFacebook = true;
		FireEvent (EventIDs.Facebook.ScoresSaved);

	}


	public void LoadScore()
	{
		if (FB.IsLoggedIn  )
		{
			CheckPermissions ();

			FB.API ("/" + FB.AppId + "/scores", HttpMethod.GET, LoadScoreResult);
			FireEvent (EventIDs.Facebook.LoadingScores);
		}else
		{
			print ("not logged in, cannot load score");
		}
	}

	void LoadScoreResult( IGraphResult result)
	{
 

		if (result.Cancelled)
		{
			print ("load score cancelled");
			FireEvent (EventIDs.Facebook.LoadScoreFailed , new PMEventArgs("Cancelled"));
			return;
		}

		if (result.Error != null)
		{
			print ("Error: " + result.Error);
			FireEvent (EventIDs.Facebook.LoadScoreFailed , new PMEventArgs("Error: " + result.Error));
			return;
		}

		ScoresData scores = new ScoresData (result.ResultDictionary);

//		print (scores.ToString ());


		// Try to find me and save to data store
		if (scores.Users.ContainsKey (FacebookDataStore.MeInfo.MyId))
		{
			FacebookUser me = scores.Users [FacebookDataStore.MeInfo.MyId];

			if (FacebookDataStore.HasUser (me.Id))
			{
				FacebookUser usr = FacebookDataStore.GetUser (me.Id);
				usr.CopyBasicInfo (me);
			} 
			else
			{
				FacebookDataStore.AddUser (me);
			}


		}

	//	print ("Facebook manager list: ");

		// Save top 10 to Data
		Data.LoadedTop10Users = new List<FacebookUser> ();
		int count = 0;
		foreach (KeyValuePair<string, FacebookUser> kvp in scores.Users)
		{
	//		print (kvp.Value.Name);

			Data.LoadedTop10Users.Add (kvp.Value);
			count++;

			if (count >= 10)
				break;	
		}

		FireEvent (EventIDs.Facebook.LoadedScores);
 
	}


	#endregion



	public void LoadPicture(FacebookUser user, int width, int height, FacebookDelegate<IGraphResult> resultDelegate)
	{

		CheckPermissions ();

		string query = string.Format ("/{0}/picture?g&width={1}&height={2}", user.Id, width, height); 
		FB.API (query, HttpMethod.GET, resultDelegate);
	}


	void OnApplicationQuit()
	{
		FacebookDataStore.SaveToFile ();
	}

	 

	#region Share

	public void Share()
	{
		FB.ShareLink( new Uri( "https://apps.facebook.com/szczelaczkaa/"), "My 4 Cannons best score!", FacebookDataStore.MeInfo.Score.ToString(), new Uri("http://i.imgur.com/aBG7S1D.png"), null) ;
	}

	public void ShareNewBestScore()
	{
		FB.ShareLink( new Uri( "https://apps.facebook.com/szczelaczkaa/"), "My 4 Cannons new best score!", FacebookDataStore.MeInfo.Score.ToString(), new Uri("http://i.imgur.com/aBG7S1D.png"), null) ;
	}

	#endregion
	
}
