using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleController : MonoBehaviour {

 
	ParticleSystem[] ps;

	void Awake()
	{
		ps = GetComponentsInChildren<ParticleSystem> ();
	}


	public void Shoot()
	{
		foreach (ParticleSystem p in ps)
		{
			if( Options.Particles )
				p.Play ();
		}
	}
}
