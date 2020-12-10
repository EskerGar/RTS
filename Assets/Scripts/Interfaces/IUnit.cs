using EpPathFinding.cs;

namespace Interfaces
{
    public interface IUnit
    {
        void Init(JumpPointParam jumpPointParam, IUnitPool unitsPool, MapGrid mapGrid);
    }
}