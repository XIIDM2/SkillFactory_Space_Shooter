using UnityEngine;

namespace Common
{
    public enum TurretMode
    {
        Primary,
        Secondary
    }
    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private SpaceShooter.Projectile m_ProjectilePrefab;
        public SpaceShooter.Projectile ProjectilePrefab => m_ProjectilePrefab;

        [SerializeField] private float m_AttackSpeedRatio;
        public float AttackSpeedRatio => m_AttackSpeedRatio;

        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;

        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;

        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }

}

