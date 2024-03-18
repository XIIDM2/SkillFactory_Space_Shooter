using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PowerUp : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            if (ship != null && Player.instance.PlayerShip)
            {
                OnPickUp(ship);
                Destroy(gameObject);
            }
        }
        protected abstract void OnPickUp(SpaceShip ship);

    }
}

