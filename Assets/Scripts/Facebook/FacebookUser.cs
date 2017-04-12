using System;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;  
[Serializable]
public class FacebookUser
{
	public string Name="";
	public long Score;
	public string Id;
	[NonSerialized]
	public Sprite Sprite; 
	public long PictureLoadTime;

	public byte[] PngPictureBytes;


	public override string ToString ()
	{
		return Id+ " " + Name +  " " + Score ;
	}


	public void CopyBasicInfo(FacebookUser u)
	{
		Name = u.Name;
		Score = u.Score;
	}

	public void LoadPicture(int width, int height)
	{

		if ( FacebookDataStore.NeedToLoadPicture(Id) == false ) 
		{
//			Debug.Log ("Time since last picture load " + (MyTime.Millis () - PictureLoadTime));
			return;
		}
//		Debug.Log ("Time To load " + (MyTime.Millis () - PictureLoadTime));
		FacebookManager.GetInstance().LoadPicture( this,100, 100, delegate(IGraphResult result)
		{ 
			//	Debug.Log ("PlayerPictureCallback");
			if (result.Error != null)
			{
				Debug.LogError ("Picture Load Error" + result.Error);
				return;
			}
			if (result.Texture == null)
			{
				
				Debug.Log ("Picture load error: no texture");
				return;
			}
			Sprite sprite = Sprite.Create( result.Texture, new Rect(0,0, result.Texture.width, result.Texture.height), new Vector2(.5f, .5f ) );

			Sprite = sprite; 
			PngPictureBytes = Sprite.texture.EncodeToPNG ();
			PictureLoadTime = MyTime.Millis();
			SEventSystem.FireEvent( EventIDs.Facebook.PictureLoaded, new PMEventArgs( Id ) );

		
		});
	}


	public void LoadPicture()
	{
		LoadPicture (200, 200);




	}


	/*
	public void test()
	{
		
		Debug.Log( Serializer.Serialize(this) );
	}
	*/




}

