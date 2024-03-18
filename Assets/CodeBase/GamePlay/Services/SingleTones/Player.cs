
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingleTonBase<Player>
    {
        public static SpaceShip SelectedSpaceship;

        [SerializeField] private int m_AmountOfLives;
        public int AmountOfLives => m_AmountOfLives;
        
        [SerializeField] private SpaceShip m_spaceShipPrefab;
        [SerializeField] private GameObject m_spaceShipExplosionPrefab;

        [SerializeField] private IncreseThurstTimer m_increseThurstTimer;

        private FollowCamera m_CameraController;
        public FollowCamera followCamera => m_CameraController;

        private ShipInputController m_ShipInputController;
        private Transform m_SpawnPoint;

        private SpaceShip m_spaceShip;
        public SpaceShip PlayerShip => m_spaceShip;

        private int m_Score;
        public int Score => m_Score;

        private int m_KillNumber;
        public int KillNumber => m_KillNumber;

        public SpaceShip SpaceShipPrefab
        {
            get
            {
                if (SelectedSpaceship == null)
                {
                    return m_spaceShipPrefab;
                }
                else
                {
                    return SelectedSpaceship;
                }
            }
        }
        public void Construct(FollowCamera followCamera, ShipInputController shipInputController, Transform spawnPoint)
        {
            m_CameraController = followCamera;
            m_ShipInputController = shipInputController;
            m_SpawnPoint = spawnPoint;
        }

        private void Start()
        {
            Respawn();
        }

        private void OnShipDeath(Vector3 pos)
        {
            m_AmountOfLives--;

            if(m_AmountOfLives > 0 )
            {
                Respawn();
            }
        }
        private void Respawn()
        {
            var NewPlayerShip = Instantiate(SpaceShipPrefab, m_SpawnPoint.transform.position, m_SpawnPoint.transform.rotation);

            m_spaceShip = NewPlayerShip.GetComponent<SpaceShip>();

            m_CameraController.SetTarget(m_spaceShip.transform);
            m_ShipInputController.SetTargetShip(m_spaceShip);
            m_increseThurstTimer.SetCurrentShip(m_spaceShip);
            m_spaceShip.SetExplosion(m_spaceShipExplosionPrefab);

            m_spaceShip.EventOnDeath.AddListener(OnShipDeath);
        }

        public void AddKill()
        {
            m_KillNumber += 1;
        }

        public void AddScore (int amount)
        {
            m_Score += amount;
        }

    }

}

