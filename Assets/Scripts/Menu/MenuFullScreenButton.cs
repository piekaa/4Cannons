using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuFullScreenButton : MonoBehaviour {

	Text text;
 

	public void OnClick()
	{
		print ("Mouse down");
		Screen.fullScreen = !Screen.fullScreen;
	}
}
