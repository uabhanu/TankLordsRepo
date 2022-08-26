using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(fileName = "NewTankData" , menuName = "Data/TankData")]
    public class TankData : ScriptableObject
    {
        public float Acceleration = 70f;
        public float Deacceleration = 50f;
        public float EngineAudioMaxVolume = 0.10f;
        public float EngineAudioMinVolume = 0.05f;
        public float EngineAudioVolumeDelta = 0.01f;
        public float MaxSpeed = 60f;
        public float RotationSpeed = 100f;
        public float TrackDistance = 0.2f;
        public GameObject TracksPrefab;
        public int KillValue;
        public int MaxHealth = 0;
        public int ObjectPoolSize;
    }
}
