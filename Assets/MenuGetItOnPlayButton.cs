using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class MenuGetItOnPlayButton : MonoBehaviour {

	public void OnClick()
	{
		//Application.OpenURL ("https://play.google.com/store/apps/details?id=piekaa.noip.pl.Szczelaczka");
		//Application.ExternalEval("window.open(\"https://play.google.com/store/apps/details?id=piekaa.noip.pl.Szczelaczka\")");	
		OpenLinkJSPlugin();
	}

	public void OpenLinkJSPlugin() {
		#if !UNITY_EDITOR
		openWindow("https://play.google.com/store/apps/details?id=piekaa.noip.pl.Szczelaczka");
		#endif
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}
