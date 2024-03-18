using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image m_FillBar;

        private float m_LastHP;
        private void Update()
        {
           float HP =  (float) Player.instance.PlayerShip.CurrentHP / (float) Player.instance.PlayerShip.StartHP;

            if (HP != m_LastHP)
            {
                m_FillBar.fillAmount = HP;
                m_LastHP = HP;
            }
        }
    }
}

