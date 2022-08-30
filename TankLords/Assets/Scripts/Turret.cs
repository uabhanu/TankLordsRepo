using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))]
public class Turret : MonoBehaviour
{
    #region Variables

    private AimTurret _aimTurret;
    private bool _canShoot = true;
    private Collider2D[] _tankColliders;
    private float _currentDelay;
    private ObjectPool _bulletPool;

    [SerializeField] private int bulletPoolCount;
    [SerializeField] private List<Transform> turretBarrels;
    [SerializeField] private UnityEvent onCanShoot;
    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private UnityEvent<float> onReloading;
    
    #endregion
    
    #region Functions

    private void Awake()
    {
        _aimTurret = GetComponentInParent<AimTurret>();
        _bulletPool = GetComponent<ObjectPool>();
        _tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Start()
    {
        _bulletPool.Initialize(_aimTurret.CurrentTurretData.BulletPrefab , bulletPoolCount);
        onReloading?.Invoke(_currentDelay);
    }

    private void Update()
    {
        if(!_canShoot)
        {
            _currentDelay -= Time.deltaTime;
            onReloading?.Invoke(_currentDelay / _aimTurret.CurrentTurretData.ReloadDelay);

            if(_currentDelay <= 0)
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
            _currentDelay = _aimTurret.CurrentTurretData.ReloadDelay;

            foreach(var barrel in turretBarrels)
            {
                //GameObject bullet = Instantiate(BulletPrefab);
                GameObject bullet = _bulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(_aimTurret.CurrentTurretData.BulletData);

                foreach(var collider in _tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>() , collider);
                }
            }
            
            onShoot?.Invoke();
            onReloading?.Invoke(_currentDelay);
        }
        else
        {
            onCanShoot?.Invoke();
        }
    }
    
    #endregion
}
