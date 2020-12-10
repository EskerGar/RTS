using System;
using Facades;
using UnityEngine;

namespace Interfaces
{
    public interface IBullet
    {
        void Init(Transform target, float damage);
    
        event Action<BulletFacade> OnReachedTarget;

        GameObject GameObject { get; }
    }
}