using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings/UnitSettings", fileName = "UnitSettings")]
    public class UnitSettings : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float maxHealth;
        [SerializeField] private float attackRadius;

        public float Speed => speed;
    
        public float Damage => damage;
    
        public float MaxHealth => maxHealth;

        public float AttackRadius => attackRadius;
    }
}