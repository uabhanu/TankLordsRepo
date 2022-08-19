using UnityEngine;

public class AudioListenerFollowTank : MonoBehaviour
{
    private Transform _transform;
    
    [SerializeField] private Transform objectToFollow;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(objectToFollow != null)
        {
            _transform.position = objectToFollow.localPosition;
        }
    }
}
