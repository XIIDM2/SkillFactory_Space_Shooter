using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class PointerClickerHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool m_Hold;
        public bool Hold => m_Hold;
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            m_Hold = true;
        }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            m_Hold = false;
        }

    }

}



