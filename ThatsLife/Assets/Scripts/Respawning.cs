using UnityEngine;

public class Respawning : MonoBehaviour
{
    private GridManager gridManager;

    private Vector2 currentPos;

    private float[] chances = { 0f, 0.02f, 0.05f, 0.10f, 0.20f, 0.15f, 0.02f, 0.0f,0f,0f,0f,0f };

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
                1
            );

            if (availableSpaces.Length == 0)
            {
                Debug.Log("No available spaces");
                return;
            }

            int rand = Random.Range(0, availableSpaces.Length);
            Vector2 place = availableSpaces[rand];

            //gridManager.PlaceObject((int)place.x, (int)place.y);
            gridManager.PlaceObject((int)availableSpaces[0].x, (int)availableSpaces[0].y);
        }
        else
        {
            //Debug.Log("No respawn");
        }
    }
}