using UnityEditor;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelComplitionPosition : LevelCondition
    {
        [SerializeField] private float m_Radius;

        public override bool IsCompleted
        {
            get 
            {
                if (Player.instance.PlayerShip == null) return false;

                if (Vector3.Distance(Player.instance.PlayerShip.transform.position, transform.position) <= m_Radius)
                {
                    return true;
                }
                return false;
            }
        }
#if UNITY_EDITOR
        private static Color GizmosColor = new Color(0, 1, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Handles.color = GizmosColor;
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
    }
#endif
}


