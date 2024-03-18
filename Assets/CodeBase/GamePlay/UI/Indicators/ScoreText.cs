using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private Text m_ScoreText;

        private int m_LastScoreText;

        private void Update()
        {
            int currentScore = Player.instance.Score;

            if (currentScore != m_LastScoreText)
            {
                m_ScoreText.text = "Score: " + currentScore.ToString();
                m_LastScoreText = currentScore;
            }
        }
    }
}

