using UnityEngine;

namespace DataSO
{
    [CreateAssetMenu(fileName = "NewCoinData" , menuName = "Data/CollectiblesData/CoinData")]
    public class CoinData : ScriptableObject
    {
        public int CoinValue;
    }
}
