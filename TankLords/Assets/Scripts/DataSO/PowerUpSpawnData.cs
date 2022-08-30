using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(fileName = "NewPowerUpSpawnData" , menuName = "Data/CollectiblesData/PowerUpSpawnData")]
    public class PowerUpSpawnData : ScriptableObject
    {
        public Color GizmosColour;
        public float Radius;
        public GameObject[] AvailablePowerUps;
    }
}
