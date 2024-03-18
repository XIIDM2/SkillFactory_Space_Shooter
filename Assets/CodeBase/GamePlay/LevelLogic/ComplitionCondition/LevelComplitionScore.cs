using UnityEngine;

namespace SpaceShooter
{
    public class LevelComplitionScore : LevelCondition
    {
        [SerializeField] private int m_Score;

        public override bool IsCompleted
        {
            get
            {
                if (Player.instance.PlayerShip == null) return false;

                if (Player.instance.Score >= m_Score)
                {
                    return true;
                }
                return false;
            }

        }

    }

}

