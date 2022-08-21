using DataSO;
using UnityEngine;

public class AimTurret : MonoBehaviour
{
    #region Variables

    [SerializeField] private TurretData turretData;

    #endregion

    #region Functions

    public void Aim(Vector2 inputPointerPosition)
    {
        var turretDirection = (Vector3)inputPointerPosition - transform.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y , turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretData.RotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation , Quaternion.Euler(0 , 0 , desiredAngle) , rotationStep);
    }
    
    public TurretData TurretData
    {
        get => turretData;
        set => turretData = value;
    }
    
    #endregion
}
