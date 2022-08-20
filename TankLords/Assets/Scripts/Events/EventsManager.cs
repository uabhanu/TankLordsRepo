using System;

namespace Events
{
	public class EventsManager
	{
		#region Actions
		
		protected static event Action CollectibleCollectedAction;
		protected static event Action EnemyDiedAction;
		
		#endregion

		#region Functions
		
		public static void SubscribeToEvent(TanksEvent evt , Action actionFunction)
		{
			switch(evt)
			{
				case TanksEvent.CollectibleCollected:
					CollectibleCollectedAction += actionFunction;
				break;
				
				case TanksEvent.EnemyDied:
					EnemyDiedAction += actionFunction;
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
				
				case TanksEvent.EnemyDied:
					EnemyDiedAction -= actionFunction;
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
				
				case TanksEvent.EnemyDied:
					EnemyDiedAction?.Invoke();
				break;
			}
		}
		
		#endregion
	}
}