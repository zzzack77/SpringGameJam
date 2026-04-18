using UnityEngine;

public class CameraMov : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Zoom")]
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 12.5f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomSmoothTime = 0.2f;

    private float targetZoom;
    private float zoomVelocity;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float edgeThreshold = 100f;
    [SerializeField] private float rightBoundry = 1406;
    [SerializeField] float rightUIWidth = 300f;

    [Header("Drag")]
    [SerializeField] private float dragSpeed = 1f;
    private Vector3 dragOrigin;

    [Header("World Bounds")]
    [SerializeField] private float worldWidth = 42.4f;
    [SerializeField] private float worldHeight = 23f;

    private float cameraX;
    private float cameraY;

    void Start()
    {
        targetZoom = mainCamera.orthographicSize;

        cameraX = transform.position.x;
        cameraY = transform.position.y;
    }

    void Update()
    {
        HandleZoom();
        HandleMovement();
        ClampCamera();

        transform.position = new Vector3(cameraX, cameraY, -10);
    }

    // ---------------- ZOOM ----------------
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scroll * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);

        mainCamera.orthographicSize = Mathf.SmoothDamp(
            mainCamera.orthographicSize,
            targetZoom,
            ref zoomVelocity,
            zoomSmoothTime
        );
    }

    // ---------------- MOVEMENT ----------------
    void HandleMovement()
    {
        
        Vector3 mouse = Input.mousePosition;

        float moveX = 0f;
        float moveY = 0f;
        //float zoomFactor = mainCamera.orthographicSize / maxZoom;
        //cameraX += moveX * zoomFactor * Time.deltaTime;
        //cameraY += moveY * zoomFactor * Time.deltaTime;

        if (mouse.x < edgeThreshold)
        {
            float t = 1f - (mouse.x / edgeThreshold);
            moveX -= t * moveSpeed;
        }
        if (mouse.x < rightBoundry && mouse.x > rightBoundry - edgeThreshold)
        {
            float dist = rightBoundry - mouse.x;
            float t = 1f - (dist / edgeThreshold);
            moveX += t * moveSpeed;
        }

        if (mouse.y < edgeThreshold)
        {
            float t = 1f - (mouse.y / edgeThreshold);
            moveY -= t * moveSpeed;
        }
        if (mouse.y > Screen.height - edgeThreshold)
        {
            float dist = Screen.height - mouse.y;
            float t = 1f - (dist / edgeThreshold);
            moveY += t * moveSpeed;
        }

        // Apply movement
        cameraX += moveX * Time.deltaTime;
        cameraY += moveY * Time.deltaTime;

        // ---------------- DRAG ----------------
        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 difference = mainCamera.ScreenToWorldPoint(dragOrigin)
                               - mainCamera.ScreenToWorldPoint(Input.mousePosition);

            cameraX += difference.x * dragSpeed;
            cameraY += difference.y * dragSpeed;

            dragOrigin = Input.mousePosition;
        }
        
    }

    // ---------------- CLAMP ----------------
    void ClampCamera()
    {
        


        float vertExtent = mainCamera.orthographicSize;
        float horzExtent = vertExtent * mainCamera.aspect;

        float minX = horzExtent;
        float maxX = worldWidth - horzExtent - (rightUIWidth / Screen.width * (horzExtent * 2));

        float minY = vertExtent;
        float maxY = worldHeight - vertExtent;

        // 🔥 FIX: if zoomed out too far, lock to center
        if (minX > maxX)
            cameraX = worldWidth / 2;
        else
            cameraX = Mathf.Clamp(cameraX, minX, maxX);

        if (minY > maxY)
            cameraY = worldHeight / 2;
        else
            cameraY = Mathf.Clamp(cameraY, minY, maxY);
    }

    // ---------------- DEBUG VISUAL ----------------
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Draw world bounds
        Gizmos.DrawWireCube(
            new Vector3(worldWidth / 2, worldHeight / 2, 0),
            new Vector3(worldWidth, worldHeight, 0)
        );
    }
}