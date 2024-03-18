using UnityEngine;

namespace Common
{
    /// <summary>
    /// Base class of all Interactive Game objects on Scene.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Name of object for User.
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;


    }
}

