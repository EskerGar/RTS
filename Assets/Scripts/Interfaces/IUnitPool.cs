using System;
using UnityEngine;

namespace Interfaces
{
    public interface IUnitPool
    {
        event Action<int> OnChangedUnitCount;

        void AddUnit(Transform unit);

        void DeleteUnit(Transform unit);

        Transform FindNearestUnit(Vector3 finderPosition);
    }
}