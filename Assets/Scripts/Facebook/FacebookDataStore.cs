using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Facebook.Unity;
using System.Runtime.Serialization.Formatters.Binary;
public class FacebookDataStore
{

	private static Dictionary<string, FacebookUser> users = new Dictionary<string, FacebookUser>();




	public static MeInfo MeInfo{ get { return meInfo; } private set { meInfo = value; } }

	private static MeInfo meInfo = new MeInfo();

	public static bool HasUser(string id )
	{
		return users.ContainsKey (id);
	}

	public static void AddUser(FacebookUser user )
	{  
		users [user.Id] = user;
	}

	public static FacebookUser GetUser(string id)
	{
		return users [id];
	}

	public static long GetUserScore(string id )
	{
		return users [id].Score;
	}


	public static Dictionary<string, FacebookUser> GetUsersDictionary()
	{
		return users;
	}


	public static bool NeedToLoadPicture(string id)
	{
		FacebookUser user;
		user = users [id];

		if (MyTime.Millis () - user.PictureLoadTime <= 1000 * 60 * 60 * 24 * 7)
			return false;
		return true;

	}

	public static void SaveMyInfoToFile()
	{
		// Save MeInfo
		try
		{
			string path = Application.persistentDataPath + "/me.bin";
			BinaryFormatter bf = new BinaryFormatter ();
			Stream stream = new FileStream (path, FileMode.Create, FileAccess.Write, FileShare.None);
			bf.Serialize (stream, meInfo);
			stream.Close ();
		}
		catch(Exception e )
		{
			Debug.Log ("Exception durring writing MeInfo file: " + e.StackTrace);
			Debug.Log ("Exception message: " + e.Message);
		}
	}

	public static void SaveToFile()
	{
		List<FacebookUser> list = new List<FacebookUser>();

		foreach (KeyValuePair<string, FacebookUser> kvp in users)
		{
			Debug.Log("Saving user: " + kvp.Value.Id );
			list.Add (kvp.Value);
		}


		// Save Users
		try
		{
			string path = Application.persistentDataPath + "/users.bin";
			BinaryFormatter bf = new BinaryFormatter ();
			Stream stream = new FileStream (path, FileMode.Create, FileAccess.Write, FileShare.None);
			bf.Serialize (stream, list);
			stream.Close ();
		}
		catch(Exception e )
		{
			Debug.Log ("Exception durring writing users file: " + e.StackTrace);
			Debug.Log ("Exception message: " + e.Message);
		}

		// Save MeInfo
		SaveMyInfoToFile();
	}

	public static void LoadFromFile()
	{
		List<FacebookUser> list;


		// Load Users
		try
		{
			string path = Application.persistentDataPath + "/users.bin";
			BinaryFormatter bf = new BinaryFormatter ();
			Stream stream = new FileStream (path, FileMode.Open, FileAccess.Read, FileShare.Read);
			list = (List<FacebookUser>) bf.Deserialize (stream);
			stream.Close ();

			users = new Dictionary<string, FacebookUser>();
			foreach (FacebookUser user in list)
			{
				Debug.Log("Loading user: " + user.Id );
				if( user.PngPictureBytes != null )
				{
					Texture2D texture = new Texture2D(2,2);
					texture.LoadImage( user.PngPictureBytes );
					user.Sprite = Sprite.Create( texture, new Rect( 0, 0,  texture.width, texture.height), new Vector2(.5f, .5f) );
				}				

				users[user.Id] =  user;
			}

		}
		catch(Exception e)
		{
			
			Debug.Log ("Exception durring loading users file: " + e.StackTrace);
			Debug.Log ("Exception message: " + e.Message);
		}



		// Load MeInfo
		try
		{
			string path = Application.persistentDataPath + "/me.bin";
			BinaryFormatter bf = new BinaryFormatter ();
			Stream stream = new FileStream (path, FileMode.Open, FileAccess.Read, FileShare.Read);
			meInfo = (MeInfo) bf.Deserialize (stream);
			stream.Close();
		}
		catch(Exception e)
		{

			Debug.Log ("Exception durring loading MeInfo file: " + e.StackTrace);
			Debug.Log ("Exception message: " + e.Message);
		}


	}




}
