using UnityEngine;
using Stickman.Managers;
using Stickman.Managers.Sound;

namespace Stickman.Damageables
{
    public class DestructibleObstacle : MonoBehaviour, IDamageable
    {
        public void HandleHit()
        {
            GameManager.Instance.SoundManager.PlayBreakBoxSound();
            Destroy(gameObject);
        }
    }
}
