using System.Collections;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class AttackComponent : MonoBehaviour, IAttack
    {
        private const int DamagePerSecond = 1;
        protected const int Delay = 1 / DamagePerSecond;

        protected float Damage;
        protected Transform Target;
        private IHealth _healthTarget;
        private Coroutine _damageCoroutine;
    
    
        public void Init(float damage)
        {
            Damage = damage; 
        }

        //<summary>
        //Получение ссылки на цель для наследуемого класса
        //</summary>
        void IAttack.SetTarget(Transform target)
        {
            Target = target;
            _healthTarget = target.GetComponent<IHealth>();
            _healthTarget.OnDied += (transform) => Target = null;
        }

        void IAttack.StartDamaging()
        {
            if(_damageCoroutine == null)
                _damageCoroutine = StartCoroutine(Damaging(_healthTarget));
        }

        void IAttack.StopDamaging()
        {
            if (_damageCoroutine == null) return;
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }

        //<summary>
        // Нанесение урона раз в delay секунд
        //</summary>
        protected virtual IEnumerator Damaging(IHealth target)
        {
            var health = target;
            const int delay = 1 / DamagePerSecond;
            while (Target != null)
            {
                health.TakeDamage(Damage);
                yield return new WaitForSeconds(delay);
                health = target;
            }

            _damageCoroutine = null;
        }
    }
}