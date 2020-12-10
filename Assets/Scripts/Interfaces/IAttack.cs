using UnityEngine;

namespace Interfaces
{
    public interface IAttack
    {

        void Init(float damage);
    
        void SetTarget(Transform target);
    
        void StartDamaging();

        void StopDamaging();
    }
}