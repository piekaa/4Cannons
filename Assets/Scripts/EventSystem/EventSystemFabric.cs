using System;

	public class EventSystemFabric
	{
		private static IEventSystem eventSystem = new EventSystem();

		public static IEventSystem GetEventSystem()
		{
			return eventSystem;
		}
	}

