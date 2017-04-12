using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDelay();


[AllowedState(StateNames.Countdown)]
[AllowedState(StateNames.Gameplay)]
public class TimeEvents : Pieka
{

	private Dictionary<int, OnDelay> delayMethods = new Dictionary<int, OnDelay>();


	void Awake()
	{ 
		Data.CurrentLevel = 1;
	}

	float tickOverhead;


	float tickLastTime;
	private int currentTick;
	public int MaxLevel = 10;

	float time = 0;

	float levelLastTime;

	float TickInterval = .1f;
	public float NextLevelInterval = 10;

	protected override void OnEnterToActiveState ()
	{
		time = 0;
		tickLastTime = time;
		delayMethods = new Dictionary<int, OnDelay>();
		currentTick = 0; 
		tickOverhead = 0;
		Data.CurrentLevel = 1;
	}


	[OnEvent( EventIDs.Game.Start)]
	void OnGameStart(string id, PMEventArgs args)
	{
		levelLastTime = time;
	}



	void SetLevel()
	{
		
	}


	protected override void OnUpdate()
	{

		time += Time.deltaTime * Time.timeScale; 

		if (time  - levelLastTime >= NextLevelInterval)
		{ 

			if (Data.CurrentLevel < MaxLevel)
			{
				levelLastTime = time; 
				Data.CurrentLevel++;
				FireEvent (EventIDs.Time.NextLevel);
			}

		}

		if (time  - tickLastTime >= TickInterval - tickOverhead)
		{ 
			tickOverhead = (time  - tickLastTime) - (TickInterval - tickOverhead); 
			tickLastTime = time;
			Data.Progress = GetProgress ();
			FireEvent (EventIDs.Time.Tick);
			currentTick++;


			if (delayMethods.ContainsKey (currentTick))
			{
				OnDelay methods = delayMethods [currentTick];

				if (methods != null)
					methods ();
			}


		}






	}



	public void ExecuteDelay(OnDelay method, int delay)
	{
		delayMethods[currentTick + delay] = method;
	}



	private float GetProgress()
	{
		float currentTime = time - levelLastTime;
 
		return currentTime / NextLevelInterval;


	}






}
