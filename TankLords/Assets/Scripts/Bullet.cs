using DataSO;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    #region Variables
    
    private float _conquaredDistance;
    private Rigidbody2D _rb2d;
    private Vector2 _startPosition;

    [SerializeField] private BulletData bulletData;
    [SerializeField] private UnityEvent onHit = new UnityEvent();

    #endregion

    #region Functions
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _conquaredDistance = Vector2.Distance(transform.position , _startPosition);

        if(_conquaredDistance >= bulletData.MaxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D col2D)
    {
        Debug.Log("Collided : " + col2D.name);
        
        onHit?.Invoke();

        var damageable = col2D.GetComponent<Damageable>();

        if(damageable != null)
        {
            damageable.Hit(bulletData.Damage);
        }
        
        DisableObject();
    }

    private void DisableObject()
    {
        _rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Initialize(BulletData bulletData)
    {
        this.bulletData = bulletData;
        _startPosition = transform.position;
        _rb2d.velocity = transform.up * this.bulletData.Speed;
    }
    
    #endregion
}
