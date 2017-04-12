using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBestScores : Pieka
{
	public MenuBestScoresUser UserPrefab;
	public List<MenuBestScoresUser> currentList = new List<MenuBestScoresUser>();

	[OnEvent(EventIDs.Lifecycle.FirstUpdate)]
	[OnEvent( EventIDs.Menu.RefreshScores) ]
	public void LoadContent()
	{

		foreach (MenuBestScoresUser user in currentList)
		{
			PiekaController.DestroyPieka (user);
		}


		currentList = new List<MenuBestScoresUser> ();


//		print ("Menu list");

		List<FacebookUser> users = FacebookManager.GetInstance ().GetFacebookUserProvider ().GetTopUsers();
		foreach (FacebookUser user in users)
		{

	//		print (user.Name);


			Pieka newPieka = PiekaController.InstantiatePieka (UserPrefab);
			newPieka.transform.parent = transform;
			MenuBestScoresUser u = (MenuBestScoresUser) newPieka;
			currentList.Add (u);
			u.transform.localScale = Vector3.one;

			u.Init (user);

		}
	}

}
