using UnityEngine;

namespace AI
{
    public class AIShootBehaviour : AIBehaviour
    {
        #region Variables
        
        private float _fieldOfVisionForShooting = 60f;
        
        #endregion

        #region Functions
        
        private bool TargetInFOV(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null) 
            {
                //TODO Missing Reference Exception even though null check made above so need to fix it later
                var direction = aiDetector.Target.position - tankController.AimTurret.transform.position;

                if(Vector2.Angle(tankController.AimTurret.transform.right , direction) < _fieldOfVisionForShooting / 2)
                {
                    return true;
                }   
            }

            return false;
        }
        
        public override void PerformAction(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null)
            {
                if(TargetInFOV(tankController , aiDetector))
                {
                    tankController.HandleMoveBody(Vector2.zero);
                    tankController.HandleShoot();
                }
            
                tankController.HandleMoveTurret(aiDetector.Target.position);   
            }
        }
        
        #endregion
    }
}
