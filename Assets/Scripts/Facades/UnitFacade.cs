using System.Collections;
using System.Collections.Generic;
using Components;
using EpPathFinding.cs;
using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Facades
{
    [RequireComponent(typeof(IMove), typeof(IHealth), typeof(IAttack))]
    public class UnitFacade : MonoBehaviour, IUnit
    { 
        [SerializeField] private UnitSettings unitSettings;

        private const float AfterSpawnDelay = .5f; // Задержка для  ожидания появления всех врагов
    
        private IPathFinder _pathFinder;
        private IMove _moveComponent;
        private IUnitPool _enemyUnitsPool;
        private IPosition _position;
        private IAttack _attack;
        private MapGrid _mapGrid;
        private Transform _prevTarget;

        void IUnit.Init(JumpPointParam jumpPointParam, IUnitPool unitsPool, MapGrid mapGrid)
        {
            ComponentsInit(jumpPointParam, mapGrid, unitsPool);
            _prevTarget = transform;
            EventsInit();
            StartCoroutine(FindNearestEnemy());
        }

        private void ComponentsInit(JumpPointParam jumpPointParam, MapGrid mapGrid, IUnitPool unitsPool)
        {
            _mapGrid = mapGrid;
            _pathFinder = new PathFinder(jumpPointParam, transform);
            _position = new Position();
            _attack = GetComponent<IAttack>();
            _enemyUnitsPool = unitsPool;
            GetComponent<IHealth>().SetMaxHealth(unitSettings.MaxHealth);
            _moveComponent = GetComponent<IMove>();
            _attack.Init(unitSettings.Damage);
            _position.SetStopDistance(unitSettings.AttackRadius);
        }

        private void EventsInit()
        {
            _position.OnStepped += _mapGrid.AllowTile;
            _position.OnNearTarget += _attack.StartDamaging;
            _position.OnFarAwayTarget += _attack.StopDamaging;
            _moveComponent.OnStepped += _mapGrid.DisallowTile;
        }

        //<summary>
        // Поиск ближайшего врага
        //</summary>
        private IEnumerator FindNearestEnemy()
        {
            var moveDelay = 1 / unitSettings.Speed;
            yield return new WaitForSeconds(AfterSpawnDelay);
            while (gameObject.activeSelf)
            {
                var currentPosition = transform.position;
                var nearestEnemy = _enemyUnitsPool.FindNearestUnit(currentPosition);
            
                if (nearestEnemy != null && !_prevTarget.Equals(nearestEnemy))
                    Attack(nearestEnemy);
            
                var enemyPath = _pathFinder.FindPathToTarget(nearestEnemy);
            
                if(enemyPath != null)
                    Move(enemyPath, currentPosition);
            
                yield return new WaitForSeconds(moveDelay);
            }
        }

        private void Attack(Transform nearestEnemy)
        {
            _attack.StopDamaging();
            _prevTarget = nearestEnemy;
            _attack.SetTarget(_prevTarget);
        }

        private void Move(List<GridPos> enemyPath, Vector3 currentPosition)
        {
            _position.SetTargetPosition(enemyPath);
            _position.SetCurrentPosition(new GridPos((int)currentPosition.x, (int)currentPosition.z));
            _moveComponent.SetPosition(_position.GetPosition());
            _position.CheckAbleFurther(new Vector3((int)currentPosition.x, 0f, (int)currentPosition.z));
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            _position.OnStepped -= _mapGrid.AllowTile;
            _moveComponent.OnStepped -= _mapGrid.DisallowTile;
            _position.OnNearTarget -= _attack.StartDamaging;
            _position.OnFarAwayTarget -= _attack.StopDamaging;
        }
    }
}