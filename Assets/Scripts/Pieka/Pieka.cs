using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using System;
using System.Reflection;  


public class PairEventLiteString
{
	public string Name;
	public PMEventLite Event;
	public PairEventLiteString (string name, PMEventLite @event)
	{
		this.Name = name;
		this.Event = @event;
	}
	
}


public class PairEventString
{
	public string Name;
	public PMEvent Event;
	public PairEventString (string name, PMEvent @event)
	{
		this.Name = name;
		this.Event = @event;
	}
	
}

public abstract class Pieka : MonoBehaviour 
{ 
	protected Dictionary<string, MethodInfo> methodsToInvokeWithParameters  = new Dictionary<string, MethodInfo>();
	protected Dictionary<string, MethodInfo> methodsToInvokeWithNoParameters = new Dictionary<string, MethodInfo>();
	protected HashSet<string> allowedStates = new HashSet<string>();

	protected List<PairEventString> events = new List<PairEventString>();
	protected List<PairEventLiteString> eventsLite = new List<PairEventLiteString>();

	protected bool isInActiveState = true;

	protected virtual void OnEnterToActiveState (){}
	protected virtual void OnExitFromActiveState (){}
	protected virtual void OnTransition(string newState){}

 

	public void OnStateChange(string name)
	{
 
		if (allowedStates.Count == 0)
			return;

		if (allowedStates.Contains (name))
		{
			if (isInActiveState == false)
			{
				OnEnterToActiveState ();
			} else
			{
				OnTransition (name);
			}
				
			isInActiveState = true;
		} else
		{
			if (isInActiveState == true)
			{
				OnExitFromActiveState ();
			}
			isInActiveState = false;
		}
	}


	protected virtual void Update()
	{
		if (isInActiveState)
			OnUpdate ();
	}

	void FixedUpdate()
	{
		if (isInActiveState)
			OnFixedUpdate ();
	}

	void LateUpdate()
	{
		if (isInActiveState)
			OnLateUpdate ();
	}


	protected virtual void OnUpdate (){}
	protected virtual void OnFixedUpdate() {}
	protected virtual void OnLateUpdate(){}

	public void Initialize()
	{ 
		StateController.AddPieka (this);


		System.Attribute[] att = (System.Attribute[])GetType ().GetCustomAttributes (true);


		foreach (System.Attribute attribute in att)
		{
			if (attribute is AllowedState)
			{
				AllowedState allowedState = (AllowedState)attribute; 
				isInActiveState = false;
				allowedStates.Add (allowedState.Name);
			}
		}

		MethodInfo[] methods = GetType ().GetMethods (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

		foreach (MethodInfo m in methods)
		{
 
			System.Attribute[] attributes = (System.Attribute[]) m.GetCustomAttributes (true);
 

			foreach (System.Attribute attribute in attributes)
			{
				if (attribute is OnEvent)
				{

					OnEvent onEventMethod = (OnEvent)attribute; 



					if (m.GetParameters ().Length == 2)
					{
						PMEvent del = (PMEvent)Delegate.CreateDelegate (typeof(PMEvent), this, m);
						events.Add ( new PairEventString( onEventMethod.Name, del));
					 
					} 
					else
					{
						PMEventLite del = (PMEventLite)Delegate.CreateDelegate (typeof(PMEventLite), this, m);
						eventsLite.Add (new PairEventLiteString(onEventMethod.Name, del));
					 
					}



				//	SEventSystem.Register ( onEventMethod.Name, (PMEvent)PMEvent.CreateDelegate( typeof(PMEvent), m ) );

				}
			}

		}
		RegisterAll ();
		OnStateChange (StateController.GetActiveState ());
	}
 
	public void Deinitialize()
	{
		Destroy (gameObject);
	}


	protected virtual void OnDestroy()
	{

		UnregisterAll ();


		allowedStates = new HashSet<string>();
		StateController.RemovePieka (this);
	}


	protected void UnregisterAll()
	{
		foreach (PairEventString e in events)
		{
			SEventSystem.Unregister (e.Name,e.Event);
		}

		foreach (PairEventLiteString e in eventsLite)
		{
			SEventSystem.Unregister (e.Name,e.Event);
		}
	}

	protected void RegisterAll()
	{
		foreach (PairEventString e in events)
		{
			SEventSystem.Register(e.Name,e.Event);
		}

		foreach (PairEventLiteString e in eventsLite)
		{
			SEventSystem.Register (e.Name,e.Event);
		}
	}


	protected void FireEvent(string id)
	{
		SEventSystem.FireEvent (id);
	}

	protected void FireEvent(string id, PMEventArgs args)
	{
		SEventSystem.FireEvent (id, args);
	}

	 
}
