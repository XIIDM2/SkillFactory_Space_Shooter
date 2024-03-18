using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        [SerializeField] private float m_VelocityDamageModifier;

        [SerializeField] private float m_Damage;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            var destructable = transform.root.GetComponent<Destructable>();

            if (destructable != null )
            {
                destructable.ApplyDamage((int) m_Damage + ((int)m_VelocityDamageModifier * (int)collision.relativeVelocity.magnitude));
            }
        }
    }
}

