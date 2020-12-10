using System;
using EpPathFinding.cs;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class MoveComponent : MonoBehaviour, IMove
    {

        public event Action<GridPos> OnStepped;
    
        private float _time;

        void IMove.SetPosition(Vector3 position)
        {
            transform.position += position;
            OnStepped?.Invoke(new GridPos((int)transform.position.x, (int)transform.position.z));
        }
    
    }
}
