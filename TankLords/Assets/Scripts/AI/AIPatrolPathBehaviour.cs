using System.Collections;
using UnityEngine;

namespace AI
{
    public class AIPatrolPathBehaviour : AIBehaviour
    {
        #region Variables
        
        private bool _isInitialized = false;
        [Range(0.1f , 1.0f)] private float _arriveDistance = 1f;
        private float _waitTime = 0.5f;
        private int _currentIndex = -1;
            
        [SerializeField] private bool isWaiting = false;
        [SerializeField] private Vector2 currentPatrolTarget = Vector2.zero;
        
        public PatrolPath PatrolPath;
        
        #endregion
        
        #region Functions

        private void Awake()
        {
            if(PatrolPath == null)
            {
                PatrolPath = GetComponentInChildren<PatrolPath>();
            }
        }
        
        private IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_waitTime);
            var nextPathPoint = PatrolPath.GetNextPathPoint(_currentIndex);
            currentPatrolTarget = nextPathPoint.Position;
            _currentIndex = nextPathPoint.Index;
            isWaiting = false;
        }

        private void CalculateDirection(TankController tankController)
        {
            Vector2 directionToGo = currentPatrolTarget - (Vector2)tankController.TankMover.transform.position;
            var dotProduct = Vector2.Dot(tankController.TankMover.transform.up , directionToGo.normalized);

            if(dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(tankController.TankMover.transform.up , directionToGo.normalized);
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                tankController.HandleMoveBody(new Vector2(rotationResult , 1));
            }
            else
            {
                tankController.HandleMoveBody(Vector2.up);
            }    
        }
        
        private void CalculateDistance(TankController tankController)
        {
            if(Vector2.Distance(tankController.transform.position , currentPatrolTarget) < _arriveDistance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
            }
        }

        private void Initialize(TankController tankController , AIDetector aiDetector)
        {
            if(!_isInitialized) //Extract into a method later
            {
                var currentPathPoint = PatrolPath.GetClosestPathPoint(tankController.transform.position);
                _currentIndex = currentPathPoint.Index;
                currentPatrolTarget = currentPathPoint.Position;
                _isInitialized = true;
            }
        }

        public override void PerformAction(TankController tankController , AIDetector aiDetector)
        {
            if(tankController != null)
            {
                if(!isWaiting)
                {
                    if(PatrolPath.Length < 2)
                    {
                        return;
                    }

                    Initialize(tankController , aiDetector);
                    CalculateDistance(tankController);
                    CalculateDirection(tankController);
                }   
            }
        }

        #endregion
    }
}
