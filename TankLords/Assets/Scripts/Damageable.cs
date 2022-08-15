using DataSO;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private int health;
    [SerializeField] private TankData tankData;
    [SerializeField] private UnityEvent onDead;
    [SerializeField] private UnityEvent onHeal;
    [SerializeField] private UnityEvent onHit;
    [SerializeField] private UnityEvent<float> onHealthChange;
    
    #endregion

    #region Functions

    private void Start()
    {
        if(Health == 0)
        {
            Health = tankData.MaxHealth;   
        }
    }

    public int Health
    {
        get
        {
            return health;
        } 
        
        set
        {
            health = value;
            onHealthChange?.Invoke((float)Health / tankData.MaxHealth);
        }
    }

    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health , 0 , tankData.MaxHealth);
        onHeal?.Invoke();
    }
    
    public void Hit(int damagePoints)
    {
        Health -= damagePoints;

        if(Health <= 0)
        {
            onDead?.Invoke();
        }
        else
        {
            onHit?.Invoke();
        }
    }
    
    #endregion
}
