using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBall: Ball 
{
	protected override void OnCollisionWithPlayer ()
	{
		FireEvent (EventIDs.BallCollision.Trap, new PMEventArgs(this));
	}
}
