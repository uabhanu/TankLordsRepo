using Events;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col2D)
    {
        EventsManager.InvokeEvent(TanksEvent.CollectibleCollected);
        Destroy(gameObject);
    }
}
