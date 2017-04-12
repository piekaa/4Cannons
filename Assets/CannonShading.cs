using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShading : MonoBehaviour {
 
	SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponentInChildren<CannonController> ().GetComponent<SpriteRenderer>();
	}
	// Update is called once per frame
	void Update () 
	{
//		MaterialPropertyBlock mpb = new MaterialPropertyBlock();
//		spriteRenderer.GetPropertyBlock (mpb);
//		mpb.SetFloat ("_Rotation", transform.rotation.eulerAngles.z);
//		spriteRenderer.SetPropertyBlock (mpb);
	}
}
