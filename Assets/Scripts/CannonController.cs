using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : Pieka {

	public ExplosionParticleController ParticleController; 
	private CannonMovementController CannonMovementController;

	private Animator animator;

	BallProvider BallProvider;

	public GameObject middle;
	public GameObject ballSpawn;


	private float ballSpeed;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator> ();	
		CannonMovementController = GetComponentInParent<CannonMovementController> ();
		BallProvider = Core.BallProvider;
	}


	public void FireCannon(float ballSpeed, float animSpeed)
	{
		this.ballSpeed = ballSpeed ;
		animator.SetFloat ("AnimSpeed", animSpeed);
		animator.SetTrigger ("Shoot");


	//	CannonMovementController.StopMoving();
	}



	[OnEvent(EventIDs.Game.End)]
	void OnEnd()
	{
		animator.enabled = false;
	}

	[OnEvent(EventIDs.Game.StartCountdown)]
	void OnCountdown()
	{ 
		animator.enabled = true;
		animator.Play ("Idle");
	}





	private void ShootBall()
	{
		Ball newBall = BallProvider.ProvideBall ();

		newBall.transform.position = ballSpawn.transform.position;
		newBall.Dirrection = (ballSpawn.transform.position - middle.transform.position).normalized;
		newBall.Speed = ballSpeed;
		newBall.Init ();	

	}

	public void Shoot()
	{
		ParticleController.Shoot ();
		ShootBall ();
		FireEvent(EventIDs.Gameplay.ShootComplete);
	}



}
