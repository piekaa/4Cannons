using System;

public class MyTime  
{
	public static long Millis()
	{
		return DateTime.Now.ToFileTime () / 10000;
	}



}
