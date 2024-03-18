using UnityEngine;
using Common;


namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructable
    {
        /// <summary>
        /// Weight for Rigid.
        /// </summary>
        [Header("Space Ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Force which moves our ship ahead.
        /// </summary>
        [SerializeField] private float m_Thurst;
        public float Thurst => m_Thurst;

        /// <summary>
        /// Timer to control SpaceShip Thurst Up.
        /// </summary>
        [SerializeField] private float m_ThurstIncreaseTimer;

        /// <summary>
        /// Rotating Force.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Max Linear Velocity.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;
        /// <summary>
        /// Max Angular Velocity.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        [SerializeField] private Sprite m_PreviewImage;

        /// <summary>
        /// saved link for Rigid.
        /// </summary>
        private Rigidbody2D m_Rigidbody;

        #region Public API

        /// <summary>
        /// Control of linear thurst. - 1.0 to + 1.0
        /// </summary>
        public float thrustControl { get; set; }

        /// <summary>
        ///  control of rotational thurst. - 1.0 to + 1.0
        /// </summary>
        public float torqueControl { get; set; }

        public Sprite PreviewImage => m_PreviewImage;

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();

            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Rigidbody.mass = m_Mass;

            m_Rigidbody.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
            ThurstControl();
            IndestructableControl();
        }

        public void AddThurst(float Amount)
        {
            m_Thurst += Amount;
        }
        public void SubstractThurst(float Amount)
        {
            m_Thurst -= Amount;
        }

        private void ThurstControl()
        {
            if (m_Thurst > 1000)
            {
                m_ThurstIncreaseTimer += Time.deltaTime;
            }

            if (m_ThurstIncreaseTimer > 5)
            {
                m_Thurst = 1000;
                m_ThurstIncreaseTimer = 0;
            }

        }

        /// <summary>
        /// Method which add Force to the ship for movement.
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigidbody.AddForce(thrustControl * m_Thurst * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody.AddForce(-m_Rigidbody.velocity * (m_Thurst / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody.AddTorque(torqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        #endregion

        [SerializeField] private Turret[] m_Turrets;
        public void Fire(TurretMode mode)
        {
            for(int i = 0; i <  m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_EnergyRegen;

        private float m_PrimaryEnergy;
        private float m_SecondaryAmmo;

        public void AddEnergy(int Energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + Energy, 0, m_MaxEnergy);
        }
        public void AddAmmo(int Ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + Ammo, 0, m_MaxAmmo);
        }


        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float) m_EnergyRegen * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        public bool DrawAmmo(int Count)
        {
            if (Count == 0)
            {
                return true;
            }

            if (m_SecondaryAmmo >= Count)
            {
                m_SecondaryAmmo -= Count;
                return true;
            }

            return false;
        }

        public bool DrawEnergy(int Count)
        {
            if (Count == 0)
            {
                return true;
            }

            if (m_PrimaryEnergy >= Count)
            {
                m_PrimaryEnergy -= Count;
                return true;
            }
                
            return false;
        }

        public void AssignWeapon(TurretProperties turretProperties)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(turretProperties);
            }
        }
    }
}

