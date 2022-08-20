using System;

namespace Events
{
	public class EventsManager
	{
		#region Actions
		
		protected static event Action CollectibleCollectedAction;
		
		#endregion

		#region Functions
		
		public static void SubscribeToEvent(TanksEvent evt , Action actionFunction)
		{
			switch(evt)
			{
				case TanksEvent.CollectibleCollected:
					CollectibleCollectedAction += actionFunction;
				break;
			}
		}

		public static void UnsubscribeFromEvent(TanksEvent evt , Action actionFunction)
		{
			switch(evt)
			{
				case TanksEvent.CollectibleCollected:
					CollectibleCollectedAction -= actionFunction;
				break;
			}
		}
		
		public static void InvokeEvent(TanksEvent evt)
		{
			switch(evt)
			{
				case TanksEvent.CollectibleCollected:
					CollectibleCollectedAction?.Invoke();
				break;
			}
		}
		
		#endregion
	}
}