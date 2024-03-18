using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class Projectile : ProjectileBase
    {
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        protected override void Update()
        {
            base.Update();
        }

        protected override void OnHit(Destructable destructable)
        {
            if (m_Parent = Player.instance.PlayerShip)
            {       
                    if (destructable.CurrentHP <= 0 )
                    {
                        Player.instance.AddScore(destructable.ScoreValue);
                        if (destructable is SpaceShip)
                        {
                            Player.instance.AddKill();
                        }

                    }
            }
        }

        protected override void OnProjectileLifeTimeEnd(Collider2D collider, Vector2 position)
        {
            if (m_ImpactEffectPrefab != null)
            {
                Instantiate(m_ImpactEffectPrefab, position, Quaternion.identity);
            }
            Destroy(gameObject, 0);
        }
    }
}

