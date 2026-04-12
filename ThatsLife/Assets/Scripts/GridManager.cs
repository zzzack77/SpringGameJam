using UnityEngine;

[System.Serializable]
public class CellData
{
    public bool isOccupied;
    public GameObject placedObject;
    public Sprite sprite;
}

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int width = 32;
    [SerializeField] private int height = 24;

    [Header("Prefabs")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject objectPrefab;

    [Header("Data")]
    [SerializeField] private Sprite seedSprite;

    public CellData[,] grid;

    void Start()
    {
        InitializeGrid();
        GenerateVisualGrid();
    }

    void Update()
    {
        HandleMouseInput();
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
        Debug.Log(GetMouseWorldPosition());
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Vector2Int gridPos = WorldToGrid(mouseWorldPos);

            if (IsWithinBounds(gridPos))
            {
                PlaceObject(gridPos.x, gridPos.y);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Vector2Int gridPos = WorldToGrid(mouseWorldPos);
            if (IsWithinBounds(gridPos))
            {
                RemoveObject(gridPos.x, gridPos.y);
            }
        }
    }

    // Place an object at the specified grid coordinates, replacing any existing object
    void PlaceObject(int x, int y)
    {
        CellData cell = grid[x, y];

        // Remove existing object
        if (cell.placedObject != null)
        {
            Destroy(cell.placedObject);
        }

        Vector3 worldPos = GridToWorld(x, y);

        GameObject obj = Instantiate(objectPrefab, worldPos, Quaternion.identity);
        //obj.GetComponent<SpriteRenderer>().sprite = seedSprite;

        cell.placedObject = obj;
        cell.isOccupied = true;
        //cell.sprite = seedSprite;

        Debug.Log($"Placed at: {x},{y}");
    }
    void RemoveObject(int x, int y)
    {
        CellData cell = grid[x, y];
        if (cell.placedObject != null)
        {
            Destroy(cell.placedObject);
            cell.placedObject = null;
            cell.isOccupied = false;
            cell.sprite = null;
        }
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