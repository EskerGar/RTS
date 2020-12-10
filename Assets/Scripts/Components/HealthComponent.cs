using System;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class HealthComponent : MonoBehaviour, IHealth
    {
        private float _maxHealth;
        private float _health;

        public event Action<Transform> OnDied;
    
        void IHealth.TakeDamage(float amount) => ChangeHealth(-amount);

        void IHealth.SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            _health = _maxHealth;
        }

        private void ChangeHealth(float amount)
        {
            _health += amount;
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            if (!(_health <= 0)) return;
            OnDied?.Invoke(transform);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDied = null; // обнуление ссылок
        }
    }
}