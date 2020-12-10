using EpPathFinding.cs;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn
{
    public abstract class SpawnPoint : MonoBehaviour
    { 
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private int spawnWidth;
        [SerializeField] private int spawnHeight;
        [SerializeField] private Color color;
        [SerializeField] private int unitsAmount;
        [SerializeField] private MapSettings mapSettings;

        private IUnitPool _unitPool;
        private IUnitPool _enemyPool;


        public SpawnPoint SetOwnUnits(IUnitPool ownUnits)
        {
            _unitPool = ownUnits;
            return this;
        }

        public SpawnPoint SetEnemyUnits(IUnitPool enemyUnits)
        {
            _enemyPool = enemyUnits;
            return this;
        }
    
    
        public void GenerateUnits(MapGrid mapGrid, JumpPointParam jumpPointParam)
        {
            for (int i = 0; i < unitsAmount; i++)
            {
                var unit = CreateUnit();
                unit.GetComponent<IHealth>().OnDied += _unitPool.DeleteUnit;
                var un = unit.GetComponent<IUnit>();
                un?.Init(jumpPointParam, _enemyPool, mapGrid);
                _unitPool.AddUnit(unit.transform);
            }
        }
    
        private GameObject CreateUnit()
        {
            return Instantiate(unitPrefab, FindPlace(), Quaternion.identity);
        }

        private bool IsAvailablePlace(Vector3 position) => 
            position.x <= mapSettings.Height && position.x >= 0 && position.z <= mapSettings.Width && position.z >= 0;

        //<summary>
        // Нахождение случайной точки спавна в границах
        //</summary>
        private Vector3 FindPlace()
        {
            var spawnPosition = transform.position;
            var halfWidth = (int)(spawnWidth * .5f);
            var halfHeight = (int)(spawnHeight * .5f);
            var position = new Vector3(Random.Range((int)spawnPosition.x - halfWidth, (int)spawnPosition.x + halfWidth), 0f,
                Random.Range((int)spawnPosition.z - halfHeight, (int)spawnPosition.z + halfHeight));
            position += new Vector3(.5f, 0, .5f);
            if (!IsAvailablePlace(position))
                position = FindPlace();
            return position;
        }

        //<summary>
        // Отображение в Editor границ спавна
        //</summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position, Vector3.one);
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnWidth, 0, spawnHeight) ); 
        }
    
    }
}
