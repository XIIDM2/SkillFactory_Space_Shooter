using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class ShipInputController : MonoBehaviour
    {
        public enum ControlMode
        {
            KeyBoard,
            JoyStick
        }

        [SerializeField] private ControlMode m_controlMode;

        private VirtualGamePad m_VirtualGamePad;

        private SpaceShip m_spaceShip;

        public void Construct(VirtualGamePad virtualGamePad)
        {
            m_VirtualGamePad = virtualGamePad;
        }

        private void Start()
        {
            if (m_controlMode == ControlMode.KeyBoard)
            {
                m_VirtualGamePad.virtualJoyStick.gameObject.SetActive(false);
                m_VirtualGamePad.mobileFirePrimary.gameObject.SetActive(false);
                m_VirtualGamePad.mobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_VirtualGamePad.virtualJoyStick.gameObject.SetActive(true);
                m_VirtualGamePad.mobileFirePrimary.gameObject.SetActive(true);
                m_VirtualGamePad.mobileFirePrimary.gameObject.SetActive(true);
            }

        }
                     

        private void Update()
        {
            if (m_spaceShip == null) return;

            if (m_controlMode == ControlMode.KeyBoard)
                ControlKeyBoard();

            if (m_controlMode == ControlMode.JoyStick)
                ControlJoyStick();
        }

        private void ControlJoyStick()
        {
            var dir = m_VirtualGamePad.virtualJoyStick.Value;
            m_spaceShip.thrustControl = dir.y;
            m_spaceShip.torqueControl = -dir.x;

            if (m_VirtualGamePad.mobileFirePrimary.Hold == true)
            {
                m_spaceShip.Fire(TurretMode.Primary);
            }

            if (m_VirtualGamePad.mobileFirePrimary.Hold == true)
            {
                m_spaceShip.Fire(TurretMode.Secondary);
            }
        }


        private void ControlKeyBoard()
        {
            float thurst = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.W))
                thurst = 1.0f;

            if (Input.GetKey(KeyCode.S))
                thurst = -1.0f;

            if (Input.GetKey(KeyCode.A))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.D))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
            {
                m_spaceShip.Fire(TurretMode.Primary);
            }

            if (Input.GetKey(KeyCode.X))
            {
                m_spaceShip.Fire(TurretMode.Secondary);
            }

            m_spaceShip.thrustControl = thurst;
            m_spaceShip.torqueControl = torque;

        }

        public void SetTargetShip(SpaceShip ship)
        {
            m_spaceShip = ship;
        }
    }
}

