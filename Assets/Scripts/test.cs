using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
 
public class test : Pieka{

    
	int time;

	int frameTime = 16;

	protected override void OnUpdate()
	{

	//	FireEvent (EventIDs.Test);

		time =  (int) (Time.time * 1000);
	}

	[OnEvent(EventIDs.Test)]
	void OnTest(){}


	void LateUpdate()
	{
	//	print (gameObject.name + " " + Time.frameCount);
		int millis = (int) (Time.time * 1000);

		if (millis - time < frameTime)
		{
		//	Thread.Sleep ( frameTime - (time - millis) );
		}
	}



}
