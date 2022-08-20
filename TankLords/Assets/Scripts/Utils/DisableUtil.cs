using UnityEngine;

namespace Utils
{
    public class DisableUtil : MonoBehaviour
    {
        public void DisableObject()
        {
            gameObject.SetActive(false);
        }
    }
}
