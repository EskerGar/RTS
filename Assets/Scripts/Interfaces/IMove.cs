using System;
using EpPathFinding.cs;
using UnityEngine;

namespace Interfaces
{
    public interface IMove
    {
        event Action<GridPos> OnStepped;

        void SetPosition(Vector3 position);
    
    }
}