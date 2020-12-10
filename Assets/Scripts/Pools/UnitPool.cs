using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Pools
{
    public class UnitPool : IUnitPool
    {
        public event Action<int> OnChangedUnitCount;
    
    
        private readonly List<Transform> _unitsPool = new List<Transform>();
    

        void IUnitPool.AddUnit(Transform unit)
        {
            _unitsPool.Add(unit);
            OnChangedUnitCount?.Invoke(_unitsPool.Count);
        }

        void IUnitPool.DeleteUnit(Transform unit)
        {
            _unitsPool.Remove(unit);
            OnChangedUnitCount?.Invoke(_unitsPool.Count);
        }

        Transform IUnitPool.FindNearestUnit(Vector3 finderPosition)
        {
            return _unitsPool?.OrderBy(u => (u.position - finderPosition).sqrMagnitude).FirstOrDefault();
        }
    
    }
}