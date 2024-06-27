using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletDamageController
    {
        internal static void DealDamage(Bullet bullet, GameObject damagable)
        {
            if (!damagable.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bullet.isPlayer == team.IsPlayer)
            {
                return;
            }

            if (damagable.TryGetComponent(out HitPointsComponent hitPoints))
            {
                Debug.Log("Дамаг");
                hitPoints.TakeDamage(bullet.damage);
            }
        }
    }
}