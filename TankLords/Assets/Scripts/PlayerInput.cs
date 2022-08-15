using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private UnityEvent onShoot;
    [SerializeField] private UnityEvent<Vector2> onMoveBody;
    [SerializeField] private UnityEvent<Vector2> onMoveTurret;
    
    #endregion
    
    #region Functions
    
    private void Update()
    {
        GetBodyMovement();
        GetShootingInput();
        GetTurretMovement();
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }
    
    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical"));
        onMoveBody?.Invoke(movementVector.normalized);
    }

    private void GetShootingInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            onShoot?.Invoke();
        }
    }

    private void GetTurretMovement()
    {
        onMoveTurret?.Invoke(GetMousePosition());   
    }
    
    #endregion
}
