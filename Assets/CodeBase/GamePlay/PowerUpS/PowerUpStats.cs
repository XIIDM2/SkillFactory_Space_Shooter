using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpStats : PowerUp
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            AddThurst,
            Indestructable

        }

        [SerializeField] private EffectType m_EffectType;
        [SerializeField] private float m_Value;
        private float m_Timer;
        
        protected override void OnPickUp(SpaceShip ship)
        {


            if (m_EffectType == EffectType.AddAmmo)
            {
                ship.AddAmmo((int) m_Value);
            }

            if (m_EffectType == EffectType.AddEnergy)
            {
                ship.AddEnergy((int) m_Value);
            }

            if (m_EffectType == EffectType.AddThurst)
            {
                ship.AddThurst((int) m_Value);
            }

            if (m_EffectType == EffectType.Indestructable)
            {
                ship.AddIndestructable();
            }
        }
       
    }
}

