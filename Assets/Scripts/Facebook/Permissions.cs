using System;
using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;
public class Permissions
{

	private HashSet<string> GrantedPermissions = new HashSet<string>();

	public Permissions(IDictionary<string,  object> rawData)
	{
		if (rawData.ContainsKey ("data"))
		{ 

			List< object > perms = (List< object>)rawData ["data"];


			foreach (IDictionary<string, object> perm in perms)
			{


				if (perm.ContainsKey ("permission") && perm.ContainsKey ("status") && (string)perm ["status"] == "granted")
				{
					GrantedPermissions.Add ( (string)perm["permission"] );
				}
			}
		}
	}


	public bool HasAllPermissions(List<string> perms )
	{
		foreach( string p in perms )
		{
			if (GrantedPermissions.Contains (p) == false)
				return false;
		}
		return true;
	}
		



	public override string ToString ()
	{
		string result = "";
		foreach (string s in GrantedPermissions)
		{
			result += s + ", ";
		}
		return result;
	}

}
 
