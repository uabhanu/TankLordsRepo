using DataSO;
using UnityEngine;

public class EngineAudio : MonoBehaviour
{
    #region Variables
    
    private AudioSource _audioSource;

    [SerializeField] private float currentVolume;
    [SerializeField] private TankData tankData;
    
    #endregion

    #region Functions

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        currentVolume = tankData.EngineAudioMinVolume;
    }

    private void Start()
    {
        _audioSource.volume = currentVolume;
    }

    public void ControlEngineVolume(float speed)
    {
        if(speed > 0)
        {
            if(currentVolume < tankData.EngineAudioMaxVolume)
            {
                currentVolume += tankData.EngineAudioVolumeDelta * Time.deltaTime;
            }
        }
        else
        {
            if(currentVolume > tankData.EngineAudioMinVolume)
            {
                currentVolume -= tankData.EngineAudioVolumeDelta * Time.deltaTime;
            }
        }

        currentVolume = Mathf.Clamp(currentVolume , tankData.EngineAudioMinVolume , tankData.EngineAudioMaxVolume);
        _audioSource.volume = currentVolume;
    }

    #endregion
}
