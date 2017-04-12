using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClockBall : Ball 
{
	protected override void OnCollisionWithPlayer ()
	{
		FireEvent (EventIDs.BallCollision.Clock, new PMEventArgs(this));
	}
}
