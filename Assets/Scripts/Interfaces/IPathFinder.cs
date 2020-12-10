using System.Collections.Generic;
using EpPathFinding.cs;
using UnityEngine;

namespace Interfaces
{
    public interface IPathFinder
    {

        List<GridPos> FindPathToTarget(Transform target);
    }
}