using System.Collections.Generic;
using Facades;
using Interfaces;
using UnityEngine;

namespace Pools
{
    public static class BulletPool
    {
    
        public static GameObject Prefab
        {
            private get => _prefab;
            set
            {
                if(_prefab == default)
                    _prefab = value;
            }
        }
    
        private static GameObject _prefab;
        private static readonly Stack<IBullet> UnActiveBulletPool = new Stack<IBullet>();

        public static IBullet GetBullet()
        {
            IBullet bullet;
            if (UnActiveBulletPool.Count > 0)
            {
                bullet = UnActiveBulletPool.Pop();
                bullet.GameObject.SetActive(true);
                return bullet;
            }

            bullet = Object.Instantiate(Prefab).GetComponent<BulletFacade>();
            bullet.OnReachedTarget += UnActiveBullet;
            return bullet;

        }

        private static void UnActiveBullet(IBullet bullet)
        {
            bullet.GameObject.SetActive(false);
            UnActiveBulletPool.Push(bullet);
        }
    }
}