using System.Collections.Generic;
using UnityEngine;
using System;


[System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
public class AllowedState : Attribute 
{
	public string Name;

	public AllowedState(string Name)
	{
		this.Name = Name;
	}
	 
}
  