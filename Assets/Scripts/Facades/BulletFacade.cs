using System;
using Interfaces;
using UnityEngine;

namespace Facades
{
    [RequireComponent(typeof(IMove), typeof(IHealth))]
    public class BulletFacade : MonoBehaviour, IBullet
    {
        public event Action<BulletFacade> OnReachedTarget;
        GameObject IBullet.GameObject => gameObject;

        private const float Offset = .5f;
    
        private IMove _moveComponent;
        private IHealth _targetHealth;
        private Transform _target;
        private float _damage;


        void IBullet.Init(Transform target, float damage)
        {
            _target = target;
            _damage = damage;
            if (_target == null) return;
            _targetHealth = target.GetComponent<IHealth>();
            _targetHealth.OnDied += DeactivatedBullet;
        }

        private void Start()
        {
            _moveComponent = GetComponent<IMove>();
        }

        private void DeactivatedBullet(Transform trans)
        {
            _target = null;
            if (_targetHealth == null) return;
            _targetHealth.OnDied -= DeactivatedBullet;
            _targetHealth = null;
        }
    
        private void Update()
        {
            if (_target == null)
            {
                OnReachedTarget?.Invoke(this);
                return;
            }
            var forward = (_target.position - transform.position).normalized;
            _moveComponent.SetPosition(forward);
            if (!((_target.position - transform.position).magnitude <= Offset)) return;
            _targetHealth.TakeDamage(_damage);
            OnReachedTarget?.Invoke(this);
        }
    
    }
}