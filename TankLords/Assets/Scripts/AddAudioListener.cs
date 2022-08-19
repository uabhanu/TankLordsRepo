using UnityEngine;

public class AddAudioListener : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void AddAudioListenerComponent()
    {
        _mainCamera.gameObject.AddComponent<AudioListener>();
    }
}
