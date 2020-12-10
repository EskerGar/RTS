using System;
using UnityEngine;

namespace Interfaces
{
    public interface IHealth
    {
        event Action<Transform> OnDied; 
    
        void TakeDamage(float amount);

        void SetMaxHealth(float maxHealth);
    }
}