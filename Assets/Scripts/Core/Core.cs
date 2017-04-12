using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour 
{
	public static BallProvider BallProvider;
	public static Cannons Cannons;
	public static Game Game;
	public static Motion Motion;
	public static Scores Scores;
	public static TimeEvents TimeEvents;
	public static Particles Particles;
	public static Pools Pools; 


	void Awake()
	{
		BallProvider = GetComponent<BallProvider> ();
		Cannons = GetComponent<Cannons> ();
		Game = GetComponent<Game> ();
		Motion = GetComponent<Motion> ();
		Scores = GetComponent<Scores> ();
		TimeEvents = GetComponent<TimeEvents> ();
		Particles = GetComponent<Particles> ();
		Pools = GetComponent<Pools> (); 
	}

}
