using UnityEngine;

namespace Common
{
    public abstract class ProjectileBase : Entity
    {
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;

        [SerializeField] private float m_LifeTime;
        [SerializeField] private int m_Damage;

        private float m_Timer;

        protected Destructable m_Parent;

        protected virtual void OnHit(Destructable destructable) { }
        protected virtual void OnHit(Collider2D collider2D) { }
        protected virtual void OnProjectileLifeTimeEnd(Collider2D collider, Vector2 position) { }
        protected virtual void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit == true)
            {
                OnHit(hit.collider);
                Destructable destructable = hit.collider.transform.root.GetComponent<Destructable>();

                if (destructable != null && destructable != m_Parent)
                {
                    destructable.ApplyDamage(m_Damage);
                    OnHit(destructable);
                }
                OnProjectileLifeTimeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime)
            {
                OnProjectileLifeTimeEnd(hit.collider, hit.point);
                m_Timer = 0f;
            }

            transform.position += new Vector3(step.x, step.y, 0);
        }
        public void SetParentShooter(Destructable Parent)
        {
            m_Parent = Parent;
        }
    }
}
