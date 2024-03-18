using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class PowerUpWeapon : PowerUp
    {
        [SerializeField] TurretProperties m_Properties;
        protected override void OnPickUp(SpaceShip ship)
        {
            ship.AssignWeapon(m_Properties);
        }
    }
}

