using UnityEngine;

namespace AI
{
    public class AIPatrolStaticBehaviour : AIBehaviour
    {
        #region Variables
        
        private float _patrolDelay = 2.5f;
        
        [SerializeField] private float currentPatrolDelay;
        [SerializeField] private Vector2 randomDirection = Vector2.zero;

        #endregion

        #region Functions
        
        private void Awake()
        {
            randomDirection = Random.insideUnitCircle;
        }

        public override void PerformAction(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null)
            {
                float angle = Vector2.Angle(tankController.AimTurret.transform.right , randomDirection);

                if(currentPatrolDelay <= 0 && (angle < 2))
                {
                    randomDirection = Random.insideUnitCircle;
                    currentPatrolDelay = _patrolDelay;
                }
                else
                {
                    if(currentPatrolDelay > 0)
                    {
                        currentPatrolDelay -= Time.deltaTime;
                    }
                    else
                    {
                        tankController.HandleMoveTurret((Vector2)tankController.AimTurret.transform.position + randomDirection);
                    }
                }   
            }
        }
        
        #endregion
    }
}
