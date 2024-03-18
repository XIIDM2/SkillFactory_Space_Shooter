using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class EntitySpawnerShip : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Entity[] m_EntityPrefabs;

        [SerializeField] private CircleArea m_CircleArea;

        [SerializeField] private SpawnMode m_SpawnMode;

        [SerializeField] private int m_SpawnAmount;

        [SerializeField] private float m_SpawnTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_SpawnTime;
        }

        private void Update()
        {
            if(m_Timer > 0)
            {
                m_Timer -= Time.deltaTime;
            }

            if(m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntities();
                m_Timer = m_SpawnTime;
            } 
        }
        private void SpawnEntities()
        {
            for (int i = 0; i < m_SpawnAmount; ++i) 
            { 
                int index = Random.Range(0, m_EntityPrefabs.Length);

                GameObject Entities = Instantiate(m_EntityPrefabs[index].gameObject);
                Entities.transform.position = m_CircleArea.GetRandomPointInZone();
            }
        }
    }

}
