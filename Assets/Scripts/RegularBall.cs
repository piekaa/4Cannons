using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBall : Ball 
{
	protected override void OnCollisionWithPlayer ()
	{
		FireEvent (EventIDs.BallCollision.Regular);
	}
}
