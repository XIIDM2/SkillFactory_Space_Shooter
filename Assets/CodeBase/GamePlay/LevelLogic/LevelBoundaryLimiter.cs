
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Level Boundary. working together with LevelBoundary script. put on object which you need to Limit.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.instance == null) return;

            var levelBoundary = LevelBoundary.instance;
            var Radius = levelBoundary.Radius;

            if ( transform.position.magnitude > Radius)
            {
                if (levelBoundary.ChosenMode == LevelBoundary.LevelBoundaryMode.Limit)
                {
                    transform.position = transform.position.normalized * Radius;
                }

                if (levelBoundary.ChosenMode == LevelBoundary.LevelBoundaryMode.Teleport)
                {
                    transform.position = -transform.position.normalized * Radius;
                }
            }

        }
    }

}

