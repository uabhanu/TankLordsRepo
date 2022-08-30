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
		protected static event Action MedicCollectedAction;
		protected static event Action<TankData , TurretData> PowerUpAction;
		
		#endregion

		#region Functions
		
		public static void SubscribeToEvent(TanksEvent tanksEvent , Action actionFunction)
		{
			switch(tanksEvent)
			{
				case TanksEvent.EnemyDied:
					EnemyDiedAction += actionFunction;
				break;
				
				case TanksEvent.MedicCollected:
					MedicCollectedAction += actionFunction;
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
		
		public static void SubscribeToEvent(TanksEvent tanksEvent , Action<TankData , TurretData> actionFunction)
		{
			switch(tanksEvent)
			{
				case TanksEvent.PowerUpCollected:
					PowerUpAction += actionFunction;
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
				
				case TanksEvent.MedicCollected:
					MedicCollectedAction -= actionFunction;
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
		
		public static void UnsubscribeFromEvent(TanksEvent tanksEvent , Action<TankData , TurretData> actionFunction)
		{
			switch(tanksEvent)
			{
				case TanksEvent.PowerUpCollected:
					PowerUpAction -= actionFunction;
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
				
				case TanksEvent.MedicCollected:
					MedicCollectedAction?.Invoke();
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
		
		public static void InvokeEvent(TanksEvent tanksEvent , TankData tankData , TurretData turretData)
		{
			switch(tanksEvent)
			{
				case TanksEvent.PowerUpCollected:
					PowerUpAction?.Invoke(tankData , turretData);
				break;
			}
		}

		#endregion
	}
}