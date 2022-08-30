using Events;
using UnityEngine;
using UnityEngine.Events;

public class MedicKit : MonoBehaviour
{
    [SerializeField] private int healthBoost;
    [SerializeField] private UnityEvent onCollected;

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        col2D.gameObject.GetComponent<Damageable>().Heal(healthBoost);
        EventsManager.InvokeEvent(TanksEvent.MedicCollected);
        onCollected?.Invoke();
    }
}
