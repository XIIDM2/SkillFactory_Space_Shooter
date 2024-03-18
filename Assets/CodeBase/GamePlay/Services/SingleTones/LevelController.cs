using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelController : SingleTonBase<LevelController>
    {
        [SerializeField] private LevelCondition[] m_LevelConditions;
        [SerializeField] private LevelProperties m_LevelProperties;

        private bool m_IsLevelCompleted;

        public UnityEvent LevelVictory;
        public UnityEvent LevelDefeat;

        public bool m_HasNextLevel => m_LevelProperties.NextLevel != null;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        private const string MainMenuSceneName = "Main_Menu";

        private void Start()
        {
            Time.timeScale = 1.0f;
            m_LevelTime = 0;
        }


        private void Update()
        {
            if (m_IsLevelCompleted == false)
            {
                CheckLevelConditions();
                m_LevelTime += Time.deltaTime;

            }
            if (Player.instance.AmountOfLives == 0)
            {
                Defeat();
            }
        }

        private void CheckLevelConditions()
        {
            int numberOfConditionsCompleted = 0;

            for (int i = 0; i < m_LevelConditions.Length; i++)
            {
                if (m_LevelConditions[i].IsCompleted == true)
                {
                    numberOfConditionsCompleted++;
                }
            }
            if (numberOfConditionsCompleted == m_LevelConditions.Length)
            {
                m_IsLevelCompleted = true;
                Debug.Log(m_IsLevelCompleted);
                Victory();
            }
        }

        private void Victory()
        {
            LevelVictory?.Invoke();
            Time.timeScale = 0.0f;
        }

        private void Defeat()
        {
            LevelDefeat?.Invoke();
            Time.timeScale = 0.0f;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(m_LevelProperties.SceneName);
        }

        public void LoadNextLevel()
        {
            if(m_HasNextLevel == true) 
            {
                SceneManager.LoadScene(m_LevelProperties.NextLevel.SceneName);
            }
            else
            {
                SceneManager.LoadScene(MainMenuSceneName);
            }
        }

    }
}

