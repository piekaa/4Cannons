using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AllowedState(StateNames.Gameplay)]
[AllowedState(StateNames.Countdown)]
[AllowedState(StateNames.BeforeStart)]
public class PlayerMovementController : Pieka 
{
 

	Rigidbody2D body;

	IPositionProvider positionProvider;

	SpriteRenderer SpriteRenderer;
 

	void Awake()
	{
		body = GetComponent<Rigidbody2D> ();
		SpriteRenderer = GetComponent<SpriteRenderer> ();
	}


	[OnEvent(EventIDs.Game.StartCountdown)]
	void OnStart()
	{  
		Cursor.visible = false;
	}
	 

	protected override void OnUpdate()
	{
//		MaterialPropertyBlock mpb = new MaterialPropertyBlock ();
//		SpriteRenderer.GetPropertyBlock(mpb);
//
//		mpb.SetFloat ("_Rotation", transform.rotation.eulerAngles.z);
//		SpriteRenderer.SetPropertyBlock( mpb );
	}
 

	protected override void OnFixedUpdate ()
	{
		Vector3 newPos = positionProvider.GetWorldPosition ();
		body.velocity = (newPos - transform.position) * 10;
 
	}

	protected override void OnEnterToActiveState ()
	{

		if (Application.isMobilePlatform)
			positionProvider = new TouchpadPositionProvider ();
		else
			positionProvider = new MousePositionProvider ();
	}

	protected override void OnExitFromActiveState ()
	{
		body.velocity = Vector2.zero;
		Cursor.visible = true;
		
	}


}
