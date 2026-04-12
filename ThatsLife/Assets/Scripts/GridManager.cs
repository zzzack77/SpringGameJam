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
    [SerializeField] private int height = 18;

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

    // =========================
    // GRID SETUP
    // =========================

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

    // =========================
    // INPUT
    // =========================

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = GetMouseWorldPosition();
            Vector2Int gridPos = WorldToGrid(mouseWorldPos);

            if (IsWithinBounds(gridPos))
            {
                PlaceObject(gridPos.x, gridPos.y);
            }
        }
    }

    // =========================
    // PLACEMENT LOGIC
    // =========================

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
        obj.GetComponent<SpriteRenderer>().sprite = seedSprite;

        cell.placedObject = obj;
        cell.sprite = seedSprite;
        cell.isOccupied = true;

        Debug.Log($"Placed at: {x},{y}");
    }

    // =========================
    // HELPER FUNCTIONS
    // =========================

    Vector3 GridToWorld(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x),
            Mathf.FloorToInt(worldPos.y)
        );
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        return pos;
    }

    bool IsWithinBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width &&
               pos.y >= 0 && pos.y < height;
    }
}