using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private int health;
    
    public int MaxHealth = 0;
    public UnityEvent OnDead;
    public UnityEvent OnHeal;
    public UnityEvent OnHit;
    public UnityEvent<float> OnHealthChange;
    
    #endregion

    #region Functions

    private void Start()
    {
        if(Health == 0)
        {
            Health = MaxHealth;   
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
            OnHealthChange?.Invoke((float)Health / MaxHealth);
        }
    }

    public void Heal(int healthBoost)
    {
        Health += healthBoost;
        Health = Mathf.Clamp(Health , 0 , MaxHealth);
        OnHeal?.Invoke();
    }
    
    public void Hit(int damagePoints)
    {
        Health -= damagePoints;

        if(Health <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }
    
    #endregion
}
