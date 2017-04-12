using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacebookUserProvider : Pieka
{
	private static List<FacebookUser> users = new List<FacebookUser>();
 

	[OnEvent (EventIDs.Facebook.LoadedScores) ]
	void OnLoadedScores()
	{

		MeInfo meInfo = FacebookDataStore.MeInfo;

		if (meInfo.Score <= FacebookDataStore.GetUser (meInfo.MyId).Score)
		{
			meInfo.Score = FacebookDataStore.GetUser (meInfo.MyId).Score;
			meInfo.HighScoreSavedToFacebook = true;
		} else
		{
			FacebookManager.GetInstance ().SaveScore (meInfo.Score);
		}


//		print ("User provider, on load");

		users = new List<FacebookUser> ();
		foreach (FacebookUser u in Data.LoadedTop10Users)
		{

//			print (u.Name);

			if (FacebookDataStore.HasUser (u.Id))
			{
				FacebookUser usr = FacebookDataStore.GetUser (u.Id);
				usr.CopyBasicInfo (u);
				users.Add (usr);
			} 
			else
			{
				FacebookDataStore.AddUser (u);
				users.Add (u);
			}
		}
 
		FireEvent (EventIDs.Menu.RefreshScores);



		foreach (FacebookUser user in users)
		{
			user.LoadPicture ();
		}

			
	}



	public void TryToFindMeAndTop10()
	{
		SortedDictionary<long, FacebookUser> sortedUsers = new SortedDictionary<long, FacebookUser>();
		
		Dictionary<string, FacebookUser> FBusers = FacebookDataStore.GetUsersDictionary ();
		foreach (KeyValuePair<string, FacebookUser> kvp in FBusers)
		{
			sortedUsers.Add (-kvp.Value.Score, kvp.Value);
		}

		users = new List<FacebookUser>();
		int index = 0;
		foreach (KeyValuePair<long, FacebookUser> kvp in sortedUsers)
		{
			users.Add (kvp.Value);
			index++;
			if (index >= 10)
				return;
		}



	}


	public long GetMyScore()
	{
		return FacebookDataStore.MeInfo.Score;
	}

	public List<FacebookUser> GetTopUsers()
	{
		return users;
	}


}
