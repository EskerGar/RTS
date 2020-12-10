using System.Collections.Generic;
using EpPathFinding.cs;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class PathFinder : IPathFinder
    {
        private readonly JumpPointParam _jumpPointParam;
        private readonly Transform _currentTransform;
    
    
        public PathFinder(JumpPointParam jumpPointParam, Transform currentTransform)
        {
            _jumpPointParam = jumpPointParam;
            _currentTransform = currentTransform;
        }

        //<summary>
        // построение пути к цели
        //</summary>
        List<GridPos> IPathFinder.FindPathToTarget(Transform target)
        {
            if (target == null) return null;
            var targetPosition = target.position;
            var currentPosition = _currentTransform.position;
            _jumpPointParam.Reset(new GridPos((int)currentPosition.x, (int)currentPosition.z), 
                new GridPos((int)targetPosition.x, (int)targetPosition.z));
            return JumpPointFinder.FindPath(_jumpPointParam);
        }
    }
}