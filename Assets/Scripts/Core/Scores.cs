using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AllowedState(StateNames.Gameplay)]
public class Scores : Pieka
{
	public long Score { get; private set;}

	private int level;

	public int BonusScoreMultiplication = 10;


	/**
	 *  Score increasment is higher per level. BonusLevelIncreasment show how much higher. 
	 * For example if BonusLevel increasment = 1.2, Score increasment on level one will be 1.2, on level 2 will be 2.4 and so on.
	*/
	public float BonusLevelIncreasment = 1;

	[OnEvent(EventIDs.Game.Start)] 
	public void OnStart()
	{

//		print ("Game start!!");

		Score = 0;
		level = 1;
	}


	public void AddBonusScore()
	{
		Score += (long) BonusLevelIncreasment * level * BonusScoreMultiplication;
	}

	[OnEvent(EventIDs.Time.Tick)]
	void IncrementScore()
	{
		Score += (long) BonusLevelIncreasment * level; 
	}

	[OnEvent(EventIDs.Time.NextLevel)]
	public void IncrementLevel()
	{
		level++;
	}


}
