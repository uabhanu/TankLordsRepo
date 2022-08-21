using System;
using DataSO;
using UnityEngine;

namespace Events
{
	public class EventsManager
	{
		#region Actions
		
		protected static event Action<AudioClip , CoinData> CoinCollectedAction;
		protected static event Action EnemyDiedAction;
		
		#endregion

		#region Functions
		
		public static void SubscribeToEvent(TanksEvent tanksEvent , Action actionFunction)
		{
			switch(tanksEvent)
			{
				case TanksEvent.EnemyDied:
					EnemyDiedAction += actionFunction;
				break;
			}
		}
		
		public static void SubscribeToEvent(TanksEvent tanksEvent , Action<AudioClip , CoinData> actionFunction)
		{
			switch(tanksEvent)
			{
				case TanksEvent.CoinCollected:
					CoinCollectedAction += actionFunction;
				break;
			}
		}

		public static void UnsubscribeFromEvent(TanksEvent tanksEvent , Action actionFunction)
		{
			switch(tanksEvent)
			{
				case TanksEvent.EnemyDied:
					EnemyDiedAction -= actionFunction;
				break;
			}
		}
		
		public static void UnsubscribeFromEvent(TanksEvent tanksEvent , Action<AudioClip , CoinData> actionFunction)
		{
			switch(tanksEvent)
			{
				case TanksEvent.CoinCollected:
					CoinCollectedAction -= actionFunction;
				break;
			}
		}
		
		public static void InvokeEvent(TanksEvent tanksEvent)
		{
			switch(tanksEvent)
			{
				case TanksEvent.EnemyDied:
					EnemyDiedAction?.Invoke();
				break;
			}
		}
		
		public static void InvokeEvent(TanksEvent tanksEvent , AudioClip clipToPlay , CoinData coinData)
		{
			switch(tanksEvent)
			{
				case TanksEvent.CoinCollected:
					CoinCollectedAction?.Invoke(clipToPlay , coinData);
				break;
			}
		}

		#endregion
	}
}