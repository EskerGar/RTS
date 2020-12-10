using System.Collections;
using Interfaces;
using Pools;
using UnityEngine;

namespace Components
{
    public class RangeAttackComponent : AttackComponent
    {
        [SerializeField]private GameObject bulletPrefab;

        protected override IEnumerator Damaging(IHealth target)
        {
            while (Target != null)
            {
                var bullet = BulletPool.GetBullet();
                bullet.GameObject.transform.position = transform.position;
                bullet.Init(Target, Damage);
                yield return new WaitForSeconds(Delay);
            }
        }

        private void Awake()
        {
            BulletPool.Prefab = bulletPrefab;
        }
    }
}