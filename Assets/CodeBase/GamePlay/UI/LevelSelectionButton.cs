using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LevelSelectionButton : MonoBehaviour
    {
        [SerializeField] private LevelProperties m_LevelProperties;
        [SerializeField] private Text m_LevelName;
        [SerializeField] private Image m_PreviewImage;

        private void Start()
        {
            if (m_LevelProperties == null) return;
            m_PreviewImage.sprite = m_LevelProperties.PreviewImage;
            m_LevelName.text = m_LevelProperties.Title;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(m_LevelProperties.SceneName);
        }
    }

}

