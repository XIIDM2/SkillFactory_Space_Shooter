using UnityEngine;
using SpaceShooter;

namespace Common
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        private Projectile m_Projectile;

        #region UNITY EVENT
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }

        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }

        }
        #endregion
        #region PUBLIC API
        public void Fire()
        {
            if (m_TurretProperties == null) return;
            if (m_RefireTimer > 0) return;

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage) == false)
            {
                return;
            }

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false)
            {
                return;
            }

            SpaceShooter.Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<SpaceShooter.Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            projectile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.AttackSpeedRatio;

            // ����� ������� ���� SFX
        }

        public void AssignLoadout(TurretProperties properties)
        {
            if (m_Mode != properties.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = properties;
        }
        #endregion
    }
}