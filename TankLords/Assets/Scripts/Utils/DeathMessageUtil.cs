using Events;
using UnityEngine;

namespace Utils
{
    public class DeathMessageUtil : MonoBehaviour
    {
        public void Send()
        {
            EventsManager.InvokeEvent(TanksEvent.EnemyDied);
        }
    }
}
