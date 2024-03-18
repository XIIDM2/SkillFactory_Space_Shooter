using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject m_Panel;
        private void Start()
        {
            m_Panel.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void ShowPause()
        {
            m_Panel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void HidePause()
        {
            m_Panel.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void LoadMainMenu()
        {
            m_Panel.SetActive(false);
            Time.timeScale = 0.0f;

            SceneManager.LoadScene(2);
        }
    }
}

