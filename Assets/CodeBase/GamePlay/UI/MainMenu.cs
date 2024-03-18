using UnityEngine;

namespace SpaceShooter
{
    public class MainMenu : MonoBehaviour
    {   
        [SerializeField] private GameObject m_MainPanel;
        [SerializeField] private GameObject m_ShipSelectionPanel;
        [SerializeField] private GameObject m_LevelSelectionPanel;

        private void Start()
        {
            m_MainPanel.SetActive(true);
        }
        public void ShowMainPanel()
        {
            m_MainPanel.SetActive(true);
            m_ShipSelectionPanel.SetActive(false);
            m_LevelSelectionPanel.SetActive(false);
        }
        public void ShowShipSelectionPanel()
        {
            m_ShipSelectionPanel.SetActive(true);
            m_MainPanel.SetActive(false);
        }
        public void ShowLevelSelectionPanel()
        {
            m_LevelSelectionPanel.SetActive(true);
            m_MainPanel.SetActive(false);
        }
        public void Exit()
        {
            Application.Quit();
        }
    }
}

