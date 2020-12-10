using System.Collections.Generic;
using System.Linq;
using EpPathFinding.cs;
using Interfaces;
using Pools;
using UnityEngine;

//<summary>
// Хранит и запускает Spawn Points
//</summary>
namespace Spawn
{
    public class SpawnSystem : MonoBehaviour
    {
        [SerializeField] private MapGrid mapGrid;
        [SerializeField] private UiCounter uiCounter;

        private IUnitPool _humansUnits;
        private IUnitPool _zombiesUnits;
        private JumpPointParam _jumpPointParam;
        private List<HumansSpawnPoint> _humansSpawnPoints = new List<HumansSpawnPoint>();
        private List<ZombiesSpawnPoint> _zombiesSpawnPoints = new List<ZombiesSpawnPoint>();
    
        private void Start()
        {
            _humansUnits = new UnitPool();
            _zombiesUnits = new UnitPool();
            _humansUnits.OnChangedUnitCount += uiCounter.RefreshHumans;
            _zombiesUnits.OnChangedUnitCount += uiCounter.RefreshZombies;
            _jumpPointParam = mapGrid.JumpPointParam;
            _humansSpawnPoints = FindObjectsOfType<HumansSpawnPoint>().ToList();
            _zombiesSpawnPoints = FindObjectsOfType<ZombiesSpawnPoint>().ToList();
            InitSpawnPoints(_humansSpawnPoints, _humansUnits, _zombiesUnits);
            InitSpawnPoints(_zombiesSpawnPoints, _zombiesUnits, _humansUnits);
        }

        private void InitSpawnPoints(IEnumerable<SpawnPoint> spawnPoints, IUnitPool ownUnits, IUnitPool enemyUnits)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                spawnPoint.SetOwnUnits(ownUnits).SetEnemyUnits(enemyUnits);
                spawnPoint.GenerateUnits(mapGrid, _jumpPointParam);
            }
        }
    }
}