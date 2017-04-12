using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.MiniJSON;

public class ScoresData 
{

	public Dictionary<string, FacebookUser> Users = new Dictionary<string, FacebookUser>();

	public ScoresData(IDictionary<string,  object> rawData)
	{
		if (rawData.ContainsKey ("data"))
		{ 

			List< object > persons = (List< object>)rawData ["data"];


			foreach (IDictionary<string, object> person in persons)
			{
				FacebookUser user = new FacebookUser ();

				if (person.ContainsKey ("score"))
				{
					user.Score = ((long)person ["score"]);

					if (person.ContainsKey("user"))
					{
						IDictionary<string, object> personInfo = (IDictionary<string, object>) person ["user"];

						if( personInfo.ContainsKey( "name" ) )
						{
							user.Name =  (string)personInfo ["name"];
						}

						if (personInfo.ContainsKey ("id"))
						{
							user.Id = (string)personInfo ["id"];
						}
					}
				}

				Users.Add (user.Id, user);

			}
		}
	}


	public override string ToString ()
	{
		string s = "";
		foreach (KeyValuePair<string, FacebookUser> kvp in Users)
		{
			s+=kvp.Value;
		}
		return s;
	}


}
