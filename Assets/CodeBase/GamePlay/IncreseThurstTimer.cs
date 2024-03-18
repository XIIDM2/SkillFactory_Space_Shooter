
using UnityEngine;
namespace SpaceShooter
{
    public class IncreseThurstTimer : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_SpaceShip;
        [SerializeField] private float AddedThrust;
        [SerializeField] private float SubstractedThrust;
        private float m_Timer;
        private void FixedUpdate()
        {
            m_Timer += Time.fixedDeltaTime;

            if (m_SpaceShip.Thurst < 3000 && m_Timer > 1)
            {
                m_SpaceShip.AddThurst(AddedThrust);
                m_Timer = 0;
            }

            if (m_SpaceShip.Thurst >= 3000)
            {
                m_SpaceShip.SubstractThurst(SubstractedThrust);
            }
        }

        public void SetCurrentShip(SpaceShip ship)
        {
            m_SpaceShip = ship;
        }
    }
}
