using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LivesText : MonoBehaviour
    {
        [SerializeField] private Text m_Livestext;

        private int m_LastLivesText;
        private void Update()
        {
            int currentlives = Player.instance.AmountOfLives;

            if (currentlives != m_LastLivesText)
            {
                m_Livestext.text = currentlives.ToString();
                m_LastLivesText = currentlives;
            }
        }


    }
}

