using Unity.VisualScripting;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("MovementV2")]
    [SerializeField] private float uiOffset = 200f;
    [SerializeField] private float[] borders = {-1.5f,32,24.5f,-1.5f};



    [Header("Zoom")]
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 12.5f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomSmoothTime = 0.2f;

    [SerializeField] private float targetZoom;
    private float zoomVelocity;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float edgeThreshold = 100f;
    [SerializeField] private float rightBoundry = 1406;
    private float borderWidth = 32;
    //[SerializeField] float rightUIWidth = 300f;

    [Header("Drag")]
    [SerializeField] private float dragSpeed = 1f;
    private Vector3 dragOrigin;

    [Header("World Bounds")]
    [SerializeField] private float worldWidth = 42.4f;
    [SerializeField] private float worldHeight = 23f;

    [SerializeField] private float cameraX;
    [SerializeField] private float cameraY;



    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private Vector3 topEdge;
    private Vector3 bottomEdge;

    void Start()
    {
        targetZoom = mainCamera.orthographicSize;

        cameraX = transform.position.x;
        cameraY = transform.position.y;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log("Mouse Screen Position: " + mousePos);

        leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height / 2f, 0f));
        rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - uiOffset, Screen.height / 2f, 0f));
        topEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height, 0f));
        bottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0f, 0f));
        Debug.Log(rightEdge.x);

        


        if (mousePos.x < edgeThreshold && targetZoom < maxZoom)
        {
            if (leftEdge.x > borders[0])
            {
                cameraX -= moveSpeed * Time.deltaTime;
                Debug.Log("left");
            }
        }
        if (mousePos.x < rightBoundry && mousePos.x > (rightBoundry - edgeThreshold) && targetZoom < maxZoom)
        {
            if (rightEdge.x < borders[1])
            {
                cameraX += moveSpeed * Time.deltaTime;
                Debug.Log("right");
            }
        }
        if (mousePos.y > Screen.height - edgeThreshold && targetZoom < maxZoom)
        {
            if (topEdge.y < borders[2])
            {
                cameraY += moveSpeed * Time.deltaTime;
                Debug.Log("up");

            }
        }
        if (mousePos.y < edgeThreshold && targetZoom < maxZoom)
        {
            if (bottomEdge.y > borders[3])
            {
                cameraY -= moveSpeed * Time.deltaTime;
                Debug.Log("down");

            }
        }

       
        //float left = Mathf.Clamp(cameraX)


        transform.position = new Vector3(cameraX, cameraY, -10);
        


        HandleZoom();
        //HandleMovement();
        //ClampCamera();
    }





    // ---------------- ZOOM ----------------
    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        

        targetZoom -= scroll * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        if (targetZoom >= maxZoom)
        {
            cameraX = Mathf.Lerp(cameraX, 21.2f, 1 * Time.deltaTime);
            cameraY = Mathf.Lerp(cameraY, 11.5f, 1 * Time.deltaTime);
        }
        mainCamera.orthographicSize = Mathf.SmoothDamp(
            mainCamera.orthographicSize,
            targetZoom,
            ref zoomVelocity,
            zoomSmoothTime
        );
        if (mainCamera.orthographicSize < targetZoom + 0.01 && mainCamera.orthographicSize > targetZoom - 0.01) return;
        if (leftEdge.x > borders[0])
        {
            cameraX -= moveSpeed * 3 * Time.deltaTime;
        }
        if (rightEdge.x < borders[1])
        {
            cameraX += moveSpeed * 3 *Time.deltaTime;
        }
        if (topEdge.y < borders[2])
        {
            cameraY += moveSpeed * 3 * Time.deltaTime;
        }
        if (bottomEdge.y > borders[3])
        {
            cameraY -= moveSpeed * 3 * Time.deltaTime;
        }
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

        // 🔥 Convert screen UI boundary into world space
        float screenRight = rightBoundry;
        float worldRight = mainCamera.ScreenToWorldPoint(new Vector3(screenRight, 0, 0)).x;

        float maxX = worldRight - horzExtent;

        float minY = vertExtent;
        float maxY = worldHeight - vertExtent;

        // Handle over-zoom case
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