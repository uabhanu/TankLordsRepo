using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PatrolPath : MonoBehaviour
    {
        #region Variables

        [Header("Gizmos Parameters")]
        private Color _lineColor = Color.magenta;
        private Color _pointsColor = Color.blue;
        private float _pointSize = 0.3f;
        
        [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
        
        public struct PathPoint
        {
            public int Index;
            public Vector2 Position;
        }
        
        #endregion
        
        #region Functions

        private void OnDrawGizmos()
        {
            if(patrolPoints.Count == 0)
            {
                return;
            }

            for(int i = patrolPoints.Count - 1; i >= 0; i--)
            {
                if(i == -1 || patrolPoints[i] == null)
                {
                    return;
                }

                Gizmos.color = _pointsColor;
                Gizmos.DrawSphere(patrolPoints[i].position , _pointSize);

                if(patrolPoints.Count == 1 || i == 0)
                {
                    return;
                }

                Gizmos.color = _lineColor;
                Gizmos.DrawLine(patrolPoints[i].position , patrolPoints[i - 1].position);

                if(patrolPoints.Count >= 2 && i == patrolPoints.Count - 1)
                {
                    Gizmos.DrawLine(patrolPoints[i].position , patrolPoints[0].position);
                }
            }
        }

        public PathPoint GetClosestPathPoint(Vector2 tankPosition)
        {
            var index = -1;
            var minDistance = float.MaxValue;

            for(int i = 0; i < patrolPoints.Count; i++)
            {
                var tempDistance = Vector2.Distance(tankPosition , patrolPoints[i].position);

                if(tempDistance < minDistance)
                {
                    minDistance = tempDistance;
                    index = i;
                }
            }

            return new PathPoint { Index = index , Position = patrolPoints[index].position };
        }

        public PathPoint GetNextPathPoint(int index)
        {
            var newIndex = index + 1 >= patrolPoints.Count ? 0 : index + 1;
            return  new PathPoint { Index = newIndex , Position = patrolPoints[newIndex].position };
        }

        public int Length
        {
            get => patrolPoints.Count;
        }
        
        #endregion
    }
}
