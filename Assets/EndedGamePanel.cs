using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AllowedState(StateNames.EndedGame)]
public class EndedGamePanel : PiekaUI {
	public NewRecordUI NewRecord;
	public UIGameplayShareButton ShareButton;


	protected override void OnEnterToActiveStateUI ()
	{
		long score = Core.Scores.Score;

		long myBestScore = FacebookManager.GetInstance ().GetFacebookUserProvider ().GetMyScore ();

		ShareButton.enabled = false;
		ShareButton.gameObject.SetActive (false);

		NewRecord.enabled = false;
		NewRecord.gameObject.SetActive (false);

		print (score);
		print (myBestScore);

		if (score > myBestScore)
		{
			NewRecord.enabled = true;
			NewRecord.gameObject.SetActive (true);

			ShareButton.enabled = true;
			ShareButton.gameObject.SetActive (true);
		}
	}


}
