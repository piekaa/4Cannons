using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AllowedState(StateNames.Countdown)]
[AllowedState(StateNames.Gameplay)] 
public class CannonMovementController : Pieka
{
 
	private float realSpeed;
	private Rigidbody2D body;
	float stoper = 0;



	private float[] speedByLevel = { 20f, 22f, 24f, 26f, 28f, 30f, 32f, 34f, 36f, 38f, 40f };
 

	void Awake()
	{
		body = GetComponent<Rigidbody2D> ();
	}


	public float MinAngle;
	public float MaxAngle;

	private int dirrection = 1;


	protected override void OnEnterToActiveState ()
	{ 
		stoper = 0;
	}

	[OnEvent(EventIDs.Game.Start)]
	void OnGameStart(string id, PMEventArgs args)
	{ 
		stoper = 1;

		realSpeed = speedByLevel[Data.CurrentLevel-1];

		body.rotation = Random.Range (MinAngle + 10, MaxAngle  - 10);

		if (Random.Range (0, 100) > 50)
			dirrection = 1;
		else
			dirrection = -1;




	}


	[OnEvent(EventIDs.Time.NextLevel)]
	void OnLevelUp(string id, PMEventArgs args)
	{
		realSpeed = speedByLevel[Data.CurrentLevel-1];	
	}

	public void StopMoving()
	{
		stoper = 0;
	}

	public void StartMoving()
	{
		stoper = 1;
	}

	 
	protected override void OnUpdate()
	{   

		body.rotation = body.rotation + (realSpeed * stoper * dirrection) * Time.timeScale * Time.deltaTime;

		if (body.rotation >= MaxAngle)
		{
			dirrection = -1;
		}
		if (body.rotation <= MinAngle)
		{
			dirrection = 1;
		}



	}


}
