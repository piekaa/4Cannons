using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTimeParticle : MonoBehaviour 
{
	ParticleSystem ps;
	float duration;
	float time;
	bool playing = false;

	public ObjectPool myPool;

	void Awake()
	{
		ps = GetComponent<ParticleSystem> ();
		duration = ps.main.duration;	
	}


	public void Play()
	{
		if (Options.Particles)
		{
			ps.Play ();
			time = 0;
			playing = true;
		}
	}


	void Update()
	{

		if (!playing)
			return;

		time += Time.deltaTime * Time.timeScale;

		if (time > duration)
		{
			myPool.PutBack (gameObject);
			//Destroy (gameObject);
		}

	}






}
