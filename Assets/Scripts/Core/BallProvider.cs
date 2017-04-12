using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProvider : MonoBehaviour {


	public Ball RegularBall;
	public Ball[] BonusBalls;

	private ObjectPool RegularBallPool;
	private ObjectPool[] BonusBallPools;



	void Awake()
	{
		BonusBallPools = new ObjectPool[ BonusBalls.Length ];


		RegularBallPool = ScriptableObject.CreateInstance< ObjectPool> ();
		RegularBall.myPool = RegularBallPool;
		RegularBallPool.InitPieka (RegularBall, 50);


		for (int i = 0; i < BonusBallPools.Length; i++)
		{
			BonusBallPools [i] = ScriptableObject.CreateInstance<ObjectPool> ();

			Ball b = BonusBalls [i];
			b.myPool = BonusBallPools [i];
			BonusBallPools [i].InitPieka (b, 10);
		}




	}

	[Range(0,1)]
	public float bonusProbability = 0.2f;


	public Ball ProvideBall()
	{
		Ball newBall;

		float rand = Random.Range (0, 100) / 100f;

		if (rand <= bonusProbability)
		{
		//	newBall = (Ball) PiekaController.InstantiatePieka (BonusBalls [Random.Range (0, BonusBalls.Length)]);
			newBall = (Ball) BonusBallPools[Random.Range (0, BonusBalls.Length)] .GetNextPieka(); 
		} 
		else
		{
		//	newBall = (Ball) PiekaController.InstantiatePieka (RegularBall);
			newBall = (Ball) RegularBallPool.GetNextPieka();
		}

		return newBall;
	}


}
