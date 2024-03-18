using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ShipSelectionButton : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_Prefab;
        [SerializeField] private MainMenu m_MainMenu;

        [SerializeField] private Text m_SpaceShipName;
        [SerializeField] private Text m_HPText;
        [SerializeField] private Text m_SpeedText;
        [SerializeField] private Text m_AgilityText;
        [SerializeField] private Image m_Prevew;

        private void Start()
        {
            if (m_Prefab == null) return;

            m_SpaceShipName.text = m_Prefab.Nickname;
            m_HPText.text = "HP: " + m_Prefab.StartHP.ToString();
            m_SpeedText.text = "Speed: " + m_Prefab.MaxLinearVelocity.ToString();
            m_AgilityText.text = "Agility: " + m_Prefab.MaxAngularVelocity.ToString();
            m_Prevew.sprite = m_Prefab.PreviewImage;

        }

        public void SelectSpaceShip()
        {
            Player.SelectedSpaceship = m_Prefab;
            m_MainMenu.ShowMainPanel();
        }

    }

}
