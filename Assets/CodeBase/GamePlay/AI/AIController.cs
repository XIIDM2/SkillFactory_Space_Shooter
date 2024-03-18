using UnityEngine;
using Common;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patroll
        }

        [Header("Patrol")]
        [SerializeField] private AIBehaviour m_AIBehaviour;
        [SerializeField] private AIPointPatrol m_AIPatrolPoint;
        [SerializeField] private bool m_IsPatrolRouteSelected;
        [SerializeField] private float m_PatrolRoutePositionRadius;
        private Vector3[] m_PatrolRoutePosition = new Vector3[3];
        private bool m_IsReturningToRoute;
        [Space(5)]

        [Header("Movement")]
        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationLinear;
        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationAngular;
        [Space(5)]

        [Header("Timers")]
        [SerializeField] private float m_SelectedMovePointTime;
        [SerializeField] private float m_AvoidCollisionSelectedMovePointTime;
        [SerializeField] private float m_FindNewTargetTime;
        [SerializeField] private float m_FindTargetDistance;
        [SerializeField] private float m_ShootDelay;
        [SerializeField] private float m_FindNewTargetPositionTime;
        [Space(15)]

        [SerializeField] private float m_AdvancePositionDistance;
        [SerializeField] private float m_EvadeRayLength;

        private Vector3 m_MovePosition;

        private Destructable m_SelectedTarget;
        private SpaceShip m_AIspaceShip;

        private Timer m_RandomizedPositionTimer;
        private Timer m_AvoidCollisionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;
        private Timer m_FindNewTargetPositionTimer;

        private void Start()
        {
            m_AIspaceShip = GetComponent<SpaceShip>();
            InitTimers();
            if (m_IsPatrolRouteSelected == true)
            {
                BuildPatrolRoute();
            }
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();
        }

        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null)
            {

            }

            if (m_AIBehaviour == AIBehaviour.Patroll) 
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            ActionAvoidCollision();
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewTarget();
            ActionFire();

        }

        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patroll) 
            {
                if (m_SelectedTarget != null && (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == false) && m_AvoidCollisionTimer.IsFinished == true)
                {
                    m_FindNewTargetPositionTimer.Start(m_FindNewTargetPositionTime);
                    m_IsReturningToRoute = false;
                    if (Player.instance.PlayerShip != null)
                    {
                        if (Player.instance.PlayerShip.thrustControl == 0)
                        {
                            m_MovePosition = m_SelectedTarget.transform.position;
                        }
                        if (Player.instance.PlayerShip.thrustControl > 0)
                        {
                            m_MovePosition = m_SelectedTarget.transform.position + m_SelectedTarget.transform.up * m_AdvancePositionDistance;
                        }
                        if (Player.instance.PlayerShip.thrustControl < 0)
                        {
                            m_MovePosition = m_SelectedTarget.transform.position + (-m_SelectedTarget.transform.up * m_AdvancePositionDistance);
                        }
                    }
                    else
                    {
                        m_MovePosition = m_SelectedTarget.transform.position;
                    }

                }
                else
                {
                    if (m_AIPatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_AIPatrolPoint.transform.position - transform.position).sqrMagnitude < m_AIPatrolPoint.Radius * m_AIPatrolPoint.Radius; 
                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizedPositionTimer.IsFinished == true)
                            {
                                m_RandomizedPositionTimer.Start(m_SelectedMovePointTime);

                                if (m_IsPatrolRouteSelected == true)
                                {
                                    if (m_FindNewTargetPositionTimer.IsFinished == true && m_IsReturningToRoute == false)
                                    {
                                        int index = Random.Range(0, m_PatrolRoutePosition.Length);

                                        m_MovePosition = (m_PatrolRoutePosition[index]);
                                        m_IsReturningToRoute = true;
                                    }
                                    if (Vector3.Distance(m_AIspaceShip.transform.position, m_PatrolRoutePosition[0]) < m_PatrolRoutePositionRadius)
                                    {
                                        m_MovePosition = m_PatrolRoutePosition[1];
                                    }
                                    if (Vector3.Distance(m_AIspaceShip.transform.position, m_PatrolRoutePosition[1]) < m_PatrolRoutePositionRadius)
                                    {
                                        m_MovePosition = m_PatrolRoutePosition[2];
                                    }
                                    if (Vector3.Distance(m_AIspaceShip.transform.position, m_PatrolRoutePosition[2]) < m_PatrolRoutePositionRadius)
                                    {
                                        m_MovePosition = m_PatrolRoutePosition[0];
                                    }
                                }
                                else
                                {
                                    
                                    Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_AIPatrolPoint.Radius + m_AIPatrolPoint.transform.position;

                                    m_MovePosition = newPoint;

                                }
                            }
                        }
                        else
                        {
                            m_MovePosition = m_AIPatrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        private void BuildPatrolRoute()
        {
            for (int i = 0; i < m_PatrolRoutePosition.Length; i++)
            {
                m_PatrolRoutePosition[i] = UnityEngine.Random.onUnitSphere * m_AIPatrolPoint.Radius + m_AIPatrolPoint.transform.position;
                m_PatrolRoutePosition[i].z = 0f;
                Debug.Log(m_PatrolRoutePosition[i]);
            }
            m_MovePosition = m_PatrolRoutePosition[0];
        }

        private void ActionAvoidCollision()
        {
            if(Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                if (m_AvoidCollisionTimer.IsFinished == true)
                {

                    m_AvoidCollisionTimer.Start(m_AvoidCollisionSelectedMovePointTime);

                    if (m_IsPatrolRouteSelected == true)
                    {
                        int index = Random.Range(0, m_PatrolRoutePosition.Length);

                        m_MovePosition = (m_PatrolRoutePosition[index]);

                    }
                    else
                    {
                        Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_AIPatrolPoint.Radius + m_AIPatrolPoint.transform.position;
                        m_MovePosition = newPoint;
                    }

                }     
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * m_EvadeRayLength);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, m_MovePosition);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(m_PatrolRoutePosition[0], m_PatrolRoutePositionRadius);
            Gizmos.DrawSphere(m_PatrolRoutePosition[1], m_PatrolRoutePositionRadius);
            Gizmos.DrawSphere(m_PatrolRoutePosition[2], m_PatrolRoutePositionRadius);
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, m_FindTargetDistance);
        }
        private void ActionControlShip()
        {
            m_AIspaceShip.thrustControl = m_NavigationLinear;
            m_AIspaceShip.torqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_AIspaceShip.transform) * m_NavigationAngular; 
        }
        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }
        private void ActionFindNewTarget()
        {
            if (m_FindNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindNearestTarget();
                m_FindNewTargetTimer.Start(m_FindNewTargetTime);
            }
        }

        private Destructable FindNearestTarget()
        {
            float MaxDist = m_FindTargetDistance;

            Destructable potentialTarget = null;

            foreach (var TargetShip in Destructable.AllDestructable)
            {
                if (TargetShip.GetComponent<SpaceShip>() == m_AIspaceShip) continue;
                if (TargetShip.TeamID == Destructable.TeamIDNeutral) continue;
                if (TargetShip.TeamID == m_AIspaceShip.TeamID) continue;

                float dist = Vector2.Distance(m_AIspaceShip.transform.position, TargetShip.transform.position);
                
                if (dist < MaxDist)
                {
                    MaxDist = dist;
                    potentialTarget = TargetShip;
                }
            }
            return potentialTarget;

        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinished == true)
                {
                    m_AIspaceShip.Fire(TurretMode.Primary);
                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patroll;
            m_AIPatrolPoint = point;
        }

        #region Timers
        private void InitTimers()
        {
            m_RandomizedPositionTimer = new Timer(m_SelectedMovePointTime);
            m_AvoidCollisionTimer = new Timer(m_AvoidCollisionSelectedMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
            m_FindNewTargetPositionTimer = new Timer(m_FindNewTargetPositionTime);


    }

        private void UpdateTimers()
        {
            m_RandomizedPositionTimer.RemoveTime(Time.deltaTime);
            m_AvoidCollisionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetPositionTimer.RemoveTime(Time.deltaTime);
        }
        #endregion
    }
}

