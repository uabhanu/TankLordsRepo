using System.Collections;
using UnityEngine;

namespace Utils
{
    public class DestroyOnAudioFinishedPlayingUtil : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_audioSource.clip.length);
            Destroy(gameObject);
        }
    }
}
