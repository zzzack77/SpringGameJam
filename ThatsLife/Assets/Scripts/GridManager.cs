using System.Linq;
using UnityEngine;

[System.Serializable]
public class CellData
{
    public bool isOccupied;
    public GameObject placedObject;
}

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 32;
    [SerializeField] private int height = 24;

    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject tileHightlight;
    [SerializeField] private GameObject[] objectPrefab;
    [SerializeField] private int currentPrefabIndex;

    public CellData[,] grid;


    private MoneyManager moneyManager;
    private bool canPlant;

    private void Awake()
    {
        moneyManager = FindAnyObjectByType<MoneyManager>();
    }
    void Start()
    {
        

        InitializeGrid();
        GenerateVisualGrid();




        
    }

    void Update()
    {
        HandleMouseInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2[] b = CheckSurroundingTilesAvailability(32,64, 2);
            for (int i = 0; i < b.Length; i++)
            {
                Debug.Log(b[i]);
            }
            Debug.Log(b.Length);
        }
    }

    public void Button(int x)
    {
        currentPrefabIndex = x;
    }



    // Set up the grid height x width and initialize each cell with default values
    void InitializeGrid()
    {
        grid = new CellData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new CellData();
            }
        }
    }

    // Create visual representation of the grid using tilePrefab
    void GenerateVisualGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = GridToWorld(x, y);
                Instantiate(tilePrefab, worldPos, Quaternion.identity);
            }
        }
    }

    // Handle mouse input to place objects on the grid
    void HandleMouseInput()
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        Vector2Int gridPos = WorldToGrid(mouseWorldPos);

        if (IsWithinBounds(gridPos))
        {
            if (tileHightlight.transform.position != GridToWorld(gridPos.x, gridPos.y))
            {
                tileHightlight.transform.position = new Vector3(gridPos.x, gridPos.y, 0);
            }

            if (Input.GetMouseButton(0))
            {
                PlaceObject(gridPos.x, gridPos.y, currentPrefabIndex);
            }
            if (Input.GetMouseButton(1))
            {
                RemoveObject(gridPos.x, gridPos.y);
            }
        }
        else if (tileHightlight.transform.position != new Vector3(-3, -3, 0))
        { 
            tileHightlight.transform.position = new Vector3(-3, -3, 0);
        } 
    }


    // Place an object at the specified grid coordinates, replacing any existing object
    public void PlaceObject(int x, int y, int index = -1, GameObject gameObject = null)
    {
        
        // index out of range check
        if (index != -1 && (index < 0 || index >= objectPrefab.Length))
        {
            Debug.LogError("Index out of range" + index);
            
            return;
        }


        CellData cell = grid[x, y];
        // Check where you want to place the object
        Vector3 worldPos = GridToWorld(x, y);

        // check if cell is ocupied
        if (cell.isOccupied) { return; }

        GameObject obj;

        // Instantiate the object depending on idex or game object
        // if they fail return
        if (index != -1) 
        { 
            GrowPlant growPlant = objectPrefab[index].GetComponent<GrowPlant>();

            if (moneyManager.money >= growPlant.plantData.seedPrice)
            {
                canPlant = true;
                MoneyManager.OnMoneySubtracted(growPlant.plantData.seedPrice);
                obj = Instantiate(objectPrefab[index], worldPos, Quaternion.identity);
            }
            else
            {
                canPlant = false;
                obj = null;
            }

        }
        else if (gameObject != null) { obj = Instantiate(gameObject, worldPos, Quaternion.identity); }
        else { return; }


        // If the instantiate passes then destroy current gameobejct in CellData and replace with new obejct
        
        // ---- remove in future maybe as unneccissary -------
        // Remove existing object
        if (cell.placedObject != null)
        {
            Destroy(cell.placedObject);
        }

        if (obj != null)
        {
            cell.placedObject = obj;
            cell.isOccupied = true;
            Debug.Log($"Placed at: {x},{y}");
        }
        

        
    }
    public void RemoveObject(int x, int y)
    {
        CellData cell = grid[x, y];
        if (cell.placedObject != null)
        {
            cell.placedObject.GetComponent<IHarvestable>().Harvest();
            Destroy(cell.placedObject);
            cell.placedObject = null;
            cell.isOccupied = false;
        }
    }
    
    public void RemoveObjectFromCell(int x, int y)
    {
        CellData cell = grid[x, y];
        if (cell.placedObject != null)
        {
            Destroy(cell.placedObject);
            cell.placedObject = null;
            cell.isOccupied = false;
        }
    }
    
    public Vector2[] CheckSurroundingTilesAvailability(int x, int y, int radius)
    {
        Vector2[] avaliable = {};

        for (int i = x - radius; i <= x + radius; i++)
        {
            for (int j = y - radius; j <= y + radius; j++)
            {
                if (IsWithinBounds(new Vector2Int(i, j)) && !grid[i, j].isOccupied)
                {
                    avaliable = avaliable.Append(new Vector2(i, j)).ToArray();
                    // Add available position to the list
                    //avaliable.Add(new Vector2(i, j));
                }
            }
        }

        return avaliable;
    }

    // Convert grid coordinates to world position (assuming each cell is 1 unit in size)
    Vector3 GridToWorld(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPos.x),
            Mathf.RoundToInt(worldPos.y)
        );
    }

    // Get the world position of the mouse cursor, with z set to 0 for 2D
    Vector3 GetMouseWorldPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    // Check if the given grid position is within the bounds of the grid
    bool IsWithinBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width &&
               pos.y >= 0 && pos.y < height;
    }

    void OnDrawGizmos()
    {
        if (grid == null) return;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = GridToWorld(x, y);
                Gizmos.color = grid[x, y].isOccupied ? Color.red : Color.green;
                Gizmos.DrawWireCube(worldPos, Vector3.one * 0.9f);
            }
        }
    }
}