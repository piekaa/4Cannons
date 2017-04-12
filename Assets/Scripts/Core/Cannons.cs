using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AllowedState(StateNames.Gameplay)]
public class Cannons : Pieka 
{
	
	public CannonController[] CannonControllers; 
	private int currentCannon;
	private float mobileMultiplier = .7f; 

	private float[] ballSpeeds = {6f, 6.5f, 7f, 7.5f, 8f, 8.5f, 9f, 9.5f, 10f, 11f  };
	private float[] animSpeeds = { 1.5f, 1.75f, 2f, 2.3f, 2.7f, 3f, 3.5f, 4f, 4.5f, 5f };  
 
	protected override void OnEnterToActiveState ()
	{ 
		currentCannon = 0;
	}

	[OnEvent(EventIDs.Game.Start)]
	[OnEvent(EventIDs.Gameplay.ShootComplete)]
	private void ShootNextCannon()
	{

		float ballSpeed = ballSpeeds[Data.CurrentLevel-1];
		float animSpeed = animSpeeds[Data.CurrentLevel-1] * Time.timeScale;


		if (Application.isMobilePlatform)
		{
			ballSpeed *= mobileMultiplier;
			animSpeed *= mobileMultiplier;
		}

		CannonControllers [currentCannon++].FireCannon (ballSpeed, animSpeed   );
 

		currentCannon %= CannonControllers.Length;
	}
  
 
}
