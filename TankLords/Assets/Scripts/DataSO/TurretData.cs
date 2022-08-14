using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(fileName = "NewTurretData" , menuName = "Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        public BulletData BulletData;
        public float ReloadDelay;
        public float RotationSpeed;
        public GameObject BulletPrefab;
    }
}
