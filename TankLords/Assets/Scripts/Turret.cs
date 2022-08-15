using DataSO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    #region Variables

    private bool _canShoot = true;
    private Collider2D[] _tankColliders;
    private ObjectPool _bulletPool;

    [SerializeField] private int bulletPoolCount;
    [SerializeField] private float currentDelay = 0f;
    [SerializeField] private List<Transform> turretBarrels;
    [SerializeField] private TurretData turretData;
    [SerializeField] private UnityEvent onCanShoot;
    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private UnityEvent<float> onReloading;
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        _bulletPool = GetComponent<ObjectPool>();
        _tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Start()
    {
        _bulletPool.Initialize(turretData.BulletPrefab , bulletPoolCount);
        onReloading?.Invoke(currentDelay);
    }

    private void Update()
    {
        if(!_canShoot)
        {
            currentDelay -= Time.deltaTime;
            onReloading?.Invoke(currentDelay / turretData.ReloadDelay); // Get the Reload Delay from Turret Data and make sure the value is between 0 & 1

            if(currentDelay <= 0)
            {
                _canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if(_canShoot)
        {
            _canShoot = false;
            currentDelay = turretData.ReloadDelay;

            foreach(var barrel in turretBarrels)
            {
                //GameObject bullet = Instantiate(BulletPrefab);
                GameObject bullet = _bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(turretData.BulletData);

                foreach(var collider in _tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>() , collider);
                }
            }
            
            onShoot?.Invoke();
            onReloading?.Invoke(currentDelay);
        }
        else
        {
            onCanShoot?.Invoke();
        }
    }
    
    #endregion
}
