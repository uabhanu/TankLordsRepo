using UnityEngine;

namespace Utils
{
    public class InstantiateUtil : MonoBehaviour
    {
        [SerializeField] private GameObject objectToInstantiate;

        public void InstantiateObject()
        {
            Instantiate(objectToInstantiate);
        }
    }
}
