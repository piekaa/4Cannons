using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AllowedState(StateNames.Gameplay)]
public class Ball : Pieka {
	

	public Vector3 Dirrection{ get; set; }
	public float Speed{ get; set; }
	[HideInInspector]
	public Vector3 StartPos;
	float rotationSpeed;
	public ObjectPool myPool;
 

	SpriteRenderer SpriteRenderer;
	Rigidbody2D body;

	void Awake()
	{
		SpriteRenderer = GetComponent<SpriteRenderer> ();
		body = GetComponent<Rigidbody2D> ();
	}

	public void Init()
	{
		StartPos = transform.position;
		rotationSpeed = Random.Range (-350f, +350f);
		//body.velocity = Dirrection * Speed;

		//body.AddForce( Dirrection * Speed, ForceMode2D.Impulse );

	}




	// Update is called once per frame
	protected override void OnFixedUpdate() 
	{ 
 

		//transform.position = transform.position + Dirrection * Speed * Time.timeScale;
		transform.position = transform.position + Dirrection * Speed * Time.timeScale * Time.fixedDeltaTime;

	//	print (Speed * Time.timeScale * Time.deltaTime);



	//	body.rotation += rotationSpeed* Time.timeScale * Time.fixedDeltaTime;
		if ((transform.position - StartPos).magnitude > 25)
		{
			ReturnMeToPool ();
		}



//		MaterialPropertyBlock mpb = new MaterialPropertyBlock ();
//		SpriteRenderer.GetPropertyBlock(mpb);
//
//		mpb.SetFloat ("_Rotation", transform.rotation.eulerAngles.z);
//		SpriteRenderer.SetPropertyBlock( mpb );
//
//

	}


	public void ReturnMeToPool()
	{
		myPool.PutBackPieka (this);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "player")
			OnCollisionWithPlayer ();
	}



	protected virtual void OnCollisionWithPlayer()
	{
	}



}
