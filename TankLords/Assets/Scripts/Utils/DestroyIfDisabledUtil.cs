using UnityEngine;

namespace Utils
{
    public class DestroyIfDisabledUtil : MonoBehaviour
    {
        public bool SelfDestructionEnabled { get; set; } = false;

        private void OnDisable()
        {
            if(SelfDestructionEnabled)
            {
                Destroy(gameObject);
            }
        }
    }
}
