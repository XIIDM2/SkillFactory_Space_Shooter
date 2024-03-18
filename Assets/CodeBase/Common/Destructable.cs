using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    /// <summary>
    /// Destroying objects on Scene. object can have HP.
    /// </summary>
    public class Destructable : Entity
    {
        #region Properties
        /// <summary>
        /// Object ignores Damage
        /// </summary>
        [SerializeField] private bool m_Indestructable;
        public bool IsIndestructable => m_Indestructable;

        /// <summary>
        /// Time for being Indestructable
        /// </summary>
        private float m_IndestructableTimer;

        /// <summary>
        /// Explosion Prefab to make visual better
        /// </summary>
        [SerializeField] private GameObject m_ExplosionPrefab;

        /// <summary>
        /// Start amount of HP
        /// </summary>
        [SerializeField] private int m_HP;
        public int StartHP => m_HP;

        /// <summary>
        /// Current amount of HP
        /// </summary>
        private int m_CurrentHP;
        public int CurrentHP => m_CurrentHP;

        #endregion

        #region Unity Events
        protected virtual void Start()
        {
            m_CurrentHP = m_HP;

            transform.SetParent(null);
        }

        #endregion

        #region Public API
        /// <summary>
        /// Apply Damage to the Object.
        /// </summary>
        /// <param name="damage"> Amount of Damage to the Object.</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructable) return;

            m_CurrentHP -= damage;

            if (m_CurrentHP <= 0)
            {
                OnDeath();
            }
        }

        public void AddIndestructable()
        {
            m_Indestructable = true;
        }

        protected void IndestructableControl()
        {
            if (m_Indestructable == true)
            {
                m_IndestructableTimer += Time.deltaTime;
            }

            if (m_IndestructableTimer > 5)
            {
                m_Indestructable = false;
                m_IndestructableTimer = 0;
            }
        }
        #endregion
        /// <summary>
        /// overrideable method of destroying object when HP <= 0.
        /// </summary>
        protected virtual void OnDeath()
        {
            m_EventOnDeath?.Invoke(transform.position);
            GameObject explosion = Instantiate(m_ExplosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 1.0f);
            Destroy(gameObject);
        }

        public void SetExplosion(GameObject explosion)
        {
            m_ExplosionPrefab = explosion;
        }

        private static HashSet<Destructable> m_AllDestructable;

        public static IReadOnlyCollection<Destructable> AllDestructable => m_AllDestructable;

        protected virtual void OnEnable()
        {
            if (m_AllDestructable == null)
            {
                m_AllDestructable = new HashSet<Destructable>();
            }
            m_AllDestructable.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructable.Remove(this);
        }

        public const int TeamIDNeutral = 0;

        [SerializeField] private int m_TeamID;
        public int TeamID => m_TeamID;

        [SerializeField] private UnityEvent<Vector3> m_EventOnDeath;
        public UnityEvent<Vector3> EventOnDeath => m_EventOnDeath;

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;
    }
}
