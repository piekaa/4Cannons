using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsController : Pieka 
{

 
	public bool Mortal = true;


	[OnEvent (EventIDs.Game.Start ) ]
	void OnGameStart()
	{
		slowMotionCounter = 0;
		trapCount = 0;
	}



	#region Dynamite
	[OnEvent(EventIDs.BallCollision.Dynamite)]
	void OnDynamite()
	{
 

		Ball[] balls = FindObjectsOfType<Ball> ();

		foreach (Ball ball in balls)
		{

//			SingleTimeParticle sparks =  Instantiate (Core.Particles.SparksParticle, ball.transform.position, Quaternion.identity);
//			SingleTimeParticle smoke = Instantiate (Core.Particles.SmokeParticle, ball.transform.position, Quaternion.identity);

			SingleTimeParticle sparks =  Core.Pools.SmokeParticles.GetNext ().GetComponent<SingleTimeParticle>();
			sparks.transform.position = ball.transform.position;
			SingleTimeParticle smoke = (SingleTimeParticle)Core.Pools.SparkParticles.GetNext ().GetComponent<SingleTimeParticle> ();
			smoke.transform.position = ball.transform.position;
			sparks.Play ();
			smoke.Play ();

			ball.ReturnMeToPool ();
		}

	}
	#endregion


	#region Slow Motion
	int slowMotionCounter;
	[OnEvent(EventIDs.BallCollision.Clock)]
	void OnClock(string id, PMEventArgs args)
	{

//		print ("Slow motion counter: " + slowMotionCounter);

//		PiekaController.DestoryPieka (args.Pieka);
		((Ball)args.Pieka).ReturnMeToPool();

		if (trapCount > 0)
			return;

		if( slowMotionCounter == 0 )
			Core.Motion.TriggerSlowMotion ();
		Core.TimeEvents.ExecuteDelay (ReturnToNormalSpeed, 8);




		slowMotionCounter++;

	}


	void ReturnToNormalSpeed()
	{
		if (trapCount > 0)
			return;
  
		slowMotionCounter--;
		if( slowMotionCounter == 0 )
			Core.Motion.TriggerNormalSpeed ();
	}
	#endregion

	#region Dolar
	[OnEvent(EventIDs.BallCollision.Dolar)]
	void OnDolar(string id, PMEventArgs args)
	{

		Core.Scores.AddBonusScore ();

		//PiekaController.DestoryPieka (args.Pieka);
		((Ball)args.Pieka).ReturnMeToPool();
	}
	#endregion


	#region Trap
	int trapCount = 0;
	[OnEvent(EventIDs.BallCollision.Trap)]
	void OnTrap(string id, PMEventArgs args)
	{
		slowMotionCounter = 0;

		if (trapCount == 0)
		{
			Core.Motion.TriggerFastSpeed ();
		}


		Core.TimeEvents.ExecuteDelay (ReturnToNormalFromTrap, 50);
		((Ball)args.Pieka).ReturnMeToPool();
		//PiekaController.DestoryPieka (args.Pieka);

		trapCount++;
	}

	void ReturnToNormalFromTrap()
	{
		trapCount--;

		if (trapCount == 0)
		{
			Core.Motion.TriggerNormalSpeed ();
		}
	}
	#endregion

	#region Regular Ball
	[OnEvent( EventIDs.BallCollision.Regular )]
	void OnNormalBall()
	{
 

		if (Mortal)
		{
			StateController.SetActiveState (StateNames.EndedGame);
			FireEvent (EventIDs.Game.End);
		}
	}
	#endregion


	#region States (dountdown end)

	[OnEvent( EventIDs.Game.CountdownEnd) ]
	void OnCountdownEnd()
	{
		StateController.SetActiveState (StateNames.Gameplay);
		FireEvent (EventIDs.Game.Start);
	}


	#endregion


	#region UI

	[OnEvent( EventIDs.GameplayUI.PlayAgainButton)]
	void OnPlayAgainButton()
	{

		FireEvent (EventIDs.Game.Reset);
		StateController.SetActiveState (StateNames.Reset);



		Ball[] balls = FindObjectsOfType<Ball>();

		foreach (Ball ball in balls)
		{
			ball.ReturnMeToPool();
		}

		StateController.SetActiveState (StateNames.Countdown);
		FireEvent (EventIDs.Game.StartCountdown);

	}


	[OnEvent( EventIDs.GameplayUI.StartButton)]
	void OnStartCountdownButton()
	{
		FireEvent (EventIDs.Game.StartCountdown);
		StateController.SetActiveState (StateNames.Countdown);
	}

	[OnEvent( EventIDs.GameplayUI.BackToMenuButton)]
	void OnBackToMenuButton()
	{
		SceneManager.LoadScene (SceneIds.Menu);
	}
	#endregion


	#region Game End
	[OnEvent( EventIDs.Game.End )]
	void OnGameEnd()
	{
		long score = Core.Scores.Score;

		long myBestScore = FacebookManager.GetInstance ().GetFacebookUserProvider ().GetMyScore ();

		if (score > myBestScore)
		{
			myBestScore = score;
			FacebookDataStore.MeInfo.Score = score;
			FacebookManager.GetInstance ().SaveScore (score);
			FacebookDataStore.SaveMyInfoToFile ();
		}

		Dictionary<string, object> data = new Dictionary<string, object> ();
		data ["score"] = score;

		Facebook.Unity.FB.LogAppEvent ("game_finished", null, data); 


	}
	#endregion


}
