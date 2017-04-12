using System;
using Facebook.Unity;
[Serializable]
public class MeInfo 
{
	public bool HasLoggedIn;
	public bool HighScoreSavedToFacebook;
	public string MyId = "-1";
	public long Score;
	public String TokenString;
}
