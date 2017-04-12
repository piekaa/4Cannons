using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : ScriptableObject
{
	Stack<GameObject> unused = new Stack<GameObject>();
	HashSet<GameObject> used = new HashSet<GameObject>();


	Stack<Pieka> unusedPieka = new Stack<Pieka>();
	HashSet<Pieka> usedPieka = new HashSet<Pieka>();


	GameObject original;
	Pieka originalPieka;


	private void AddNew()
	{
		GameObject obj = Instantiate (original);
		obj.SetActive (false);
		unused.Push (obj);
	}

	private void AddNewPieka()
	{
		Pieka pieka = PiekaController.InstantiatePieka (originalPieka);
		pieka.gameObject.SetActive (false);
		unusedPieka.Push (pieka);
	}


	public void Init(GameObject original, int size)
	{
		this.original = original;
		for(int i = 0  ; i  < size ; i++)
		{
			AddNew ();
		}
	}


	public void InitPieka(Pieka original, int size )
	{
		originalPieka = original;

		for(int i = 0 ; i < size ; i++)
		{
			AddNewPieka ();	
		}
	}



	public GameObject GetNext()
	{ 

		if (unused.Count == 0)
		{
			Debug.Log("ObjectPool is to small!! " + this.GetType ().Name);
			AddNew ();
		}


		GameObject obj = unused.Pop ();

		obj.SetActive (true);

		used.Add (obj);

		return obj;
	}

	public Pieka GetNextPieka()
	{

		if (unusedPieka.Count == 0)
		{
			Debug.Log("ObjectPool is to small!! " + this.GetType ().Name);
			Debug.Log ("Used pieka: " + usedPieka.Count);
			AddNewPieka ();
		}

		Pieka pieka = unusedPieka.Pop ();

		pieka.gameObject.SetActive (true);

		usedPieka.Add (pieka);

		return pieka;

	}



	public void PutBack(GameObject obj)
	{
		obj.SetActive (false);
		used.Remove (obj);
		unused.Push (obj);
	}

	public void PutBackPieka(Pieka pieka)
	{
		pieka.gameObject.SetActive (false);
		usedPieka.Remove (pieka);
		unusedPieka.Push (pieka);
	}



	


}
