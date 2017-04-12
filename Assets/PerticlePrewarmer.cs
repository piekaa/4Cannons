using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerticlePrewarmer : MonoBehaviour 
{

	bool first = true;

	void Update()
	{

		if (first)
		{
			first = false;
			ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem> (); 
		 
			foreach (ParticleSystem ps in particleSystems)
			{
				ps.Emit (50);
			}

		}
	}



}
