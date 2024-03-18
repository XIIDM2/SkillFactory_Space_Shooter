using UnityEngine;

namespace Common
{
    public class ImpactEffect : MonoBehaviour
    {
        [SerializeField] private float m_LifeTime;

        private float m_Timer;

        private void Update()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime)
            {
                Destroy(gameObject);
                m_Timer = 0f;
            }
        }
    }
}

