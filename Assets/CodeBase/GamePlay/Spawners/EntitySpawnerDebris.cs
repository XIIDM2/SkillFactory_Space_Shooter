using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class EntitySpawnDebris : MonoBehaviour
    {
        [SerializeField] private Destructable[] m_DebrisPrefab;

        [SerializeField] private CircleArea m_CircleArea;

        private GameObject Debris;

        [SerializeField] private int m_SpawnDebrisAmount;
        [SerializeField] private int m_SpawnDebrisFragmentAmount;

        [SerializeField] private float m_Speed;

        private void Start()
        {
           for (int i = 0; i < m_SpawnDebrisAmount; i++ )
           {
                SpawnDebris();
           }
        }
       
        private void SpawnDebris()
        {
            int index = Random.Range(0, m_DebrisPrefab.Length);

            Debris = Instantiate(m_DebrisPrefab[index].gameObject);
            Debris.transform.position = m_CircleArea.GetRandomPointInZone();

            Debris.GetComponent<Destructable>().EventOnDeath.AddListener(OnDebrisDestroyed);
            
            Rigidbody2D rigidbody = Debris.GetComponent<Rigidbody2D>();

            if (rigidbody != null && m_Speed > 0 )
            {
                rigidbody.velocity = (Vector2) Random.insideUnitSphere * m_Speed;
            }
        }

        private void SpawnDebrisFragment(Vector3 pos)
        {
            int index = Random.Range(0, m_DebrisPrefab.Length);

            GameObject DebrisFragment = Instantiate(m_DebrisPrefab[index].gameObject);
            DebrisFragment.transform.position = pos;
            DebrisFragment.transform.localScale = new Vector3(0.50f, 0.50f, 0.50f);

            Rigidbody2D rigidbody = DebrisFragment.GetComponent<Rigidbody2D>();

            if (rigidbody != null && m_Speed > 0)
            {
                rigidbody.velocity = (Vector2)Random.insideUnitSphere * m_Speed;
            }
        }

        private void OnDebrisDestroyed(Vector3 pos)
        {
            for (int i = 0; i < m_SpawnDebrisFragmentAmount; i++)
            {
                SpawnDebrisFragment(pos);
            }
            SpawnDebris();           
        }

    }
}

