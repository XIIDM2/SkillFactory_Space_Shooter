using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class KillText : MonoBehaviour
    {
        [SerializeField] private Text m_KillText;

        private int m_LastKLillText;

        private void Update()
        {
            int currentKills = Player.instance.KillNumber;

            if (currentKills != m_LastKLillText)
            {
                m_KillText.text = "Kills: " + currentKills.ToString();
                m_LastKLillText = currentKills;
            }
        }
    }
}


