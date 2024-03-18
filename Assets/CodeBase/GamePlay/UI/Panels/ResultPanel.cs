using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] private Text m_ResultText;
        [SerializeField] private Text m_KillsAmountText;
        [SerializeField] private Text m_ScoreText;
        [SerializeField] private Text m_TimeText;
        [SerializeField] private Text m_ButtonNextText;

        private bool m_IsLevelaPassed;
        private void Start()
        {
            gameObject.SetActive(false);
            LevelController.instance.LevelVictory.AddListener(OnLevelVictory);
            LevelController.instance.LevelDefeat.AddListener(OnLevelDefeat);

        }
        private void OnDestroy()
        {
            LevelController.instance.LevelVictory.RemoveListener(OnLevelVictory);
            LevelController.instance.LevelDefeat.RemoveListener(OnLevelDefeat);
        }

        private void OnLevelVictory()
        {
            gameObject.SetActive(true);

            m_IsLevelaPassed = true;

            m_ResultText.text = "Victory";

            ShowStatistics();

            if (LevelController.instance.m_HasNextLevel == true)
            {
                m_ButtonNextText.text = "Next Level";
            }
            else
            {
                m_ButtonNextText.text = "Main Menu";
            }

        }

        private void OnLevelDefeat()
        {
            gameObject.SetActive(true);

            m_ResultText.text = "Defeat";

            ShowStatistics();

            m_ButtonNextText.text = "Restart";


        }

        private void ShowStatistics()
        {
            m_KillsAmountText.text = "Kills: " + Player.instance.KillNumber.ToString();

            m_ScoreText.text = "Score: " + Player.instance.Score.ToString();

            m_TimeText.text = "Time: " + LevelController.instance.LevelTime.ToString("F0");
        }

        public void OnButtonAction()
        {
            if (m_IsLevelaPassed == true)
            {
                LevelController.instance.LoadNextLevel();
            }
            else
            {
                LevelController.instance.RestartLevel();
            }
        }
    }
}
