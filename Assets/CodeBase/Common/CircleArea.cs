using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using Common;

namespace SpaceShooter
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public Vector2 GetRandomPointInZone()
        {
            return (Vector2) transform.position + (Vector2) UnityEngine.Random.insideUnitSphere * m_Radius;
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

