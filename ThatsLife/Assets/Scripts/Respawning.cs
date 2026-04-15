using UnityEngine;

public class Respawning : MonoBehaviour
{
    private GridManager gridManager;

    private Vector2 currentPos;

    private float[] chances = { 0f, 0.0f, 0.01f, 0.2f, 0.5f, 0.2f, 0.005f, 0.0f,0f,0f,0f,0f };

    private void Awake()
    {
        //gridManager = GetComponent<GridManager>();
        //gridManager = FindAnyObjectByType<GridManager>();
        gridManager = FindAnyObjectByType<GridManager>();
        currentPos = transform.position;

        Vector2[] availableSpaces = gridManager.CheckSurroundingTilesAvailability(
                (int)transform.position.x,
                (int)transform.position.y,
                4
            );

        Debug.Log($"Available spaces at start: {availableSpaces.Length}"); 
    }

    public void Respawn(int stageIndex)
    {
        if (stageIndex < 0 || stageIndex >= chances.Length)
            return;

        if (Random.value < chances[stageIndex])
        {
            Vector2[] availableSpaces = gridManager.CheckSurroundingTilesAvailability(
                (int)transform.position.x,
                (int)transform.position.y,
                4
            );

            if (availableSpaces.Length == 0)
            {
                Debug.Log("No available spaces");
                return;
            }

            int rand = Random.Range(0, availableSpaces.Length);
            Vector2 place = availableSpaces[rand];

            //gridManager.PlaceObject((int)place.x, (int)place.y);
            gridManager.PlaceObject((int)availableSpaces[rand].x, (int)availableSpaces[rand].y, -1, this.gameObject);
        }
        else
        {
            //Debug.Log("No respawn");
        }
    }
}