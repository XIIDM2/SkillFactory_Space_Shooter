using UnityEngine;
using Common;


namespace SpaceShooter
{
    public class HomingMissle : Projectile
    {
        [SerializeField] private SpaceShip m_spaceShip;

        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        [SerializeField] private float m_Range;
        public float Range => m_Range;

        [SerializeField] private float m_InterpolationAngular;

        private Destructable m_SelectedTarget;

        protected override void Update()
        {
            FindNearestTarget();
            HomeToTarget();
            base.Update();

        }

        private void HomeToTarget()
        {   
            if (m_SelectedTarget != null)
            {
                Vector3 direction = m_SelectedTarget.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -angle), Time.deltaTime * m_InterpolationAngular);
            }
        }

        private void FindNearestTarget()
        {
            foreach (var TargetShip in Destructable.AllDestructable)
            {
                if (TargetShip.GetComponent<SpaceShip>() == m_spaceShip) continue;
                if (TargetShip.TeamID == Destructable.TeamIDNeutral) continue;
                if (TargetShip.TeamID == m_spaceShip.TeamID) continue;

                Vector3 dir = transform.position - TargetShip.transform.position;

                float dist = dir.magnitude;

                if (dist < m_Radius)
                {
                    m_SelectedTarget = TargetShip;
                }               
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + transform.up * m_Range, m_Radius);
        }
    }
}

