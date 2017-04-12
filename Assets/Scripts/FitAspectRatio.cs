using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitAspectRatio : MonoBehaviour {


	private float MajfonSize = 5.5f;


	void setCameraSize()
	{
		float h = Screen.height / ((float)Screen.width / 16f);



		Camera c = GetComponent<Camera> ();


		if (h <= 9)
		{
			c.orthographicSize = MajfonSize;
			return;
		}


		float size = MajfonSize * h / 9f;

		c.orthographicSize = size ;
	}



	// Use this for initialization
	void Start () 
	{
		setCameraSize ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
