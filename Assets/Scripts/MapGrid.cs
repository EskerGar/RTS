using EpPathFinding.cs;
using ScriptableObjects;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private MapSettings mapSettings;

    public JumpPointParam JumpPointParam { get; private set; }

    private BaseGrid _searchGrid;

    public void DisallowTile(GridPos gridPos) => _searchGrid.SetWalkableAt(gridPos, false);

    public void AllowTile(GridPos gridPos) => _searchGrid.SetWalkableAt(gridPos, true);

    private void Awake()
    {
            CreateGrid();
            GenerateGrid();
    }

    //<summary>
    // генерация поля
    //</summary>
    private void CreateGrid()
    {
            _searchGrid = new StaticGrid(mapSettings.Width, mapSettings.Height, GetMovableMatrix(mapSettings.Width, mapSettings.Height));
            JumpPointParam = new JumpPointParam(_searchGrid, EndNodeUnWalkableTreatment.ALLOW, DiagonalMovement.IfAtLeastOneWalkable );
    }

    //<summary>
    // создание тайлов
    //</summary>
    private void GenerateGrid()
    {
            for(int i = 0; i < mapSettings.Width; i++)
                    for (int j = 0; j < mapSettings.Height; j++)
                    {
                            var go = Instantiate(tile, transform);
                            var position = new Vector3(0.5f, 0, 0.5f) + new Vector3(i, 0, j);
                            go.transform.position = position;
                    }
    }

    private bool[][] GetMovableMatrix(int width, int height)
    { 
            var movableMatrix = new bool [width][];
            for(int widthTrav = 0; widthTrav < width; widthTrav++)
            {
                    movableMatrix[widthTrav] = new bool[height];
                    for(int heightTrav = 0; heightTrav < height;  heightTrav++)
                    { 
                            movableMatrix[widthTrav][heightTrav] = true; 
                    }  
            }

            return movableMatrix;
    }
        
}