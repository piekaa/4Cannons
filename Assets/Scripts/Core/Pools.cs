using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour 
{
	public ObjectPool SmokeParticles;
	public ObjectPool SparkParticles;


	void Start()
	{ 	
		

		SmokeParticles = ScriptableObject.CreateInstance<ObjectPool> ();
		Core.Particles.SmokeParticle.myPool = SmokeParticles;
		SmokeParticles.Init ( Core.Particles.SmokeParticle.gameObject , 40);

		SparkParticles = ScriptableObject.CreateInstance<ObjectPool> ();
		Core.Particles.SparksParticle.myPool = SparkParticles;
		SparkParticles.Init (Core.Particles.SparksParticle.gameObject, 40);

	}





	 
}
