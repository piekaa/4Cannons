using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBall : Ball 
{
	protected override void OnCollisionWithPlayer ()
	{ 
		FireEvent (EventIDs.BallCollision.Dynamite, new PMEventArgs(gameObject));
	}
}
