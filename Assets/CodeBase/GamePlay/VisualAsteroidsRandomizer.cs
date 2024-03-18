using UnityEngine;

namespace SpaceShooter
{
    public class VisualAsteroidsRandomizer : MonoBehaviour
    {
        [SerializeField] private Sprite[] m_Asteroids;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = m_Asteroids[Random.Range(0, m_Asteroids.Length)];
        }
    }
}

