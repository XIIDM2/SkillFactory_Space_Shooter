using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawnerPowerUps : MonoBehaviour
    {
        [SerializeField] private CircleArea m_CircleArea;

        [SerializeField] private int m_SpawnAmount;

        [SerializeField] private PowerUpStats[] m_PowerUpStats;
        [SerializeField] private PowerUpWeapon[] m_PowerUpWeapons;

        private void Start ()
        {
            for (int i = 0; i < m_SpawnAmount; i++)
            {
                SpawnPowerUpsWeapon();
                SpawnPowerUpsStats();
            }
        }

        private void SpawnPowerUpsWeapon()
        {
            int index = Random.Range(0, m_PowerUpWeapons.Length);

            GameObject PowerUpWeapons = Instantiate(m_PowerUpWeapons[index].gameObject);
            PowerUpWeapons.transform.position = m_CircleArea.GetRandomPointInZone();

        }

        private void SpawnPowerUpsStats()
        {
            int index = Random.Range(0, m_PowerUpStats.Length);

            GameObject PowerUpStats = Instantiate(m_PowerUpStats[index].gameObject);
            PowerUpStats.transform.position = m_CircleArea.GetRandomPointInZone();
        }
    }
}

