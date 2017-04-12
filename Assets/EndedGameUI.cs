using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AllowedState(StateNames.EndedGame)]
public class EndedGameUI : PiekaUI 
{
	public GameObject NewRecord;
	public GameObject EndedGame;


	protected override void OnEnterToActiveStateUI ()
	{
		long score = Core.Scores.Score;

		long myBestScore = FacebookManager.GetInstance ().GetFacebookUserProvider ().GetMyScore ();
 
		EndedGame.SetActive (false);
 
		NewRecord.SetActive (false);

	//	print (score);
	//	print (myBestScore);

		if (score > myBestScore)
		{ 
			NewRecord.SetActive (true);

		} else
		{
			EndedGame.SetActive (true);
		}
	}
}
