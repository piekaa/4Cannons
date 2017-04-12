using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolarBall : Ball 
{
	protected override void OnCollisionWithPlayer ()
	{
		FireEvent (EventIDs.BallCollision.Dolar, new PMEventArgs(this));
	}
}
