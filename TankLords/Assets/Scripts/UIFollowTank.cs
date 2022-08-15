using UnityEngine;

public class UIFollowTank : MonoBehaviour
{
    private RectTransform _rectTransform;
    
    [SerializeField] private Transform objectToFollow;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(objectToFollow != null)
        {
            _rectTransform.anchoredPosition = objectToFollow.localPosition;
        }
    }
}
