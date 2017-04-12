using System;
using UnityEngine;


[AllowedState(StateNames.Gameplay)]
public class Motion : Pieka
{
	 
	float targetSpeed = 1;


	public float ChangingSpeed = 0.5f;


	public float slowSpeed = 0.3f;
	
	public float fastSpeed = 2f;

	public void TriggerSlowMotion()
	{
		targetSpeed = slowSpeed;
	}

	public void TriggerNormalSpeed()
	{
		targetSpeed = 1;
	}

	public void TriggerFastSpeed()
	{
		targetSpeed = fastSpeed;
	}

	void Reset()
	{
		targetSpeed = 1;
		setTimeSclae(1);
	}

	protected override void OnEnterToActiveState ()
	{
		Reset ();
	}

	protected override void OnExitFromActiveState ()
	{
		Reset ();
	}


	private void setTimeSclae(float scale)
	{
		Time.timeScale = scale;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

	private void changeTimeScale(float delta)
	{
		
		Time.timeScale = Time.timeScale + delta;
		Time.fixedDeltaTime = 0.02f * Time.timeScale;
	}

	protected override void OnUpdate()
	{



		if (Time.timeScale > targetSpeed)
		{

			if (Time.timeScale - ChangingSpeed < targetSpeed)
			{
				setTimeSclae (targetSpeed);
			} else
			{
				changeTimeScale (-ChangingSpeed);
			}
		} else
		{
			if (Time.timeScale < targetSpeed)
			{
				if (Time.timeScale + ChangingSpeed > targetSpeed)
				{
					setTimeSclae (targetSpeed);
				} else
				{
					changeTimeScale (ChangingSpeed);
				}
			}
		}
 
		/*
		if (slowingDown)
		{

			if (Time.timeScale <= slowSpeed)
			{
				setTimeSclae (slowSpeed);
				slowingDown = false;
			} else
			{
				changeTimeScale (-ChangingSpeed);
			}


		}

		if (speedingUp)
		{
			if (Time.timeScale >= 1)
			{
				setTimeSclae (1);
				speedingUp = false;
			} else
			{
				changeTimeScale (ChangingSpeed);
			}
		}
		*/
	}




}

