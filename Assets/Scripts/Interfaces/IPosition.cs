using System;
using System.Collections.Generic;
using EpPathFinding.cs;
using UnityEngine;

namespace Interfaces
{
    public interface IPosition
    {
        event Action<GridPos> OnStepped;

        event Action OnNearTarget;

        event Action OnFarAwayTarget;

        Vector3 GetPosition();

        void SetStopDistance(float distance);

        void SetTargetPosition(List<GridPos> gridPoses);

        void SetCurrentPosition(GridPos position);

        void CheckAbleFurther(Vector3 currentPosition);
    }
}