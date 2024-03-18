using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : SingleTonBase<LevelBoundary>
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public enum LevelBoundaryMode
        {
            Limit,
            Teleport
        }

        [SerializeField] private LevelBoundaryMode m_Mode;
        public LevelBoundaryMode ChosenMode => m_Mode;

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
        #endif
    }

}

