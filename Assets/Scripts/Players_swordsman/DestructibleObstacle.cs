using UnityEngine;

namespace Stickman.Damageables
{
    public class DestructibleObstacle : MonoBehaviour, IDamageable
    {
        public void HandleHit()
        {
            Destroy(gameObject);
        }
    }
}
