using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;


public class MenuBestScoresUser : Pieka {

	public Text Scores;
	public Text Name;
	public Image Picture;

	private FacebookUser me;
 


	public void Init(FacebookUser user)
	{ 
		Scores.text = user.Score.ToString ();
		Name.text = user.Name;
		me = user;  
		if (me.Sprite != null)
		{
			Picture.overrideSprite = me.Sprite;
		}
	}
	/*
	[OnEvent(EventIDs.MouseClick ) ]
	public void OnMouseClick()
	{
		print ("On mouse click " + GetHashCode ());
	}
	*/

	[OnEvent( EventIDs.Facebook.PictureLoaded) ]
	public void OnPictureLoaded(string id, PMEventArgs args)
	{

		if (me == null)
			return; 

		if (me.Id == args.Text)
		{ 
			Picture.overrideSprite = me.Sprite;
		}
	}
 

}
