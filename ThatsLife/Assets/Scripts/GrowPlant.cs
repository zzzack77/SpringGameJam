using System;
using UnityEngine;

public class GrowPlant : MonoBehaviour, IGrowable
{
    
    [SerializeField] private PlantData plantData;
    private Respawning respawning;

    private SpriteRenderer spriteRenderer;

    private float growthTimer = 0;
    int stageCount;
    int stageIndex;

    private float respawnTimer = 0f;
    private float respawnInterval = 0.1f;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawning = FindAnyObjectByType<Respawning>();
    }

    public void Grow()
    {
        //Debug.Log(plantData.plantName);

        spriteRenderer.sprite = ChangeSprite();

        respawnTimer += Time.deltaTime;
        if (respawnTimer >= respawnInterval)
        {
            respawnTimer = 0f;
            respawning.Respawn(stageIndex);
        }
    }

    private Sprite ChangeSprite()
    {
        growthTimer += Time.deltaTime;

        float growthProgress = growthTimer / plantData.growthTime;

        stageCount = plantData.plantSprites.Length;
        stageIndex = Mathf.Clamp(Mathf.FloorToInt(growthProgress * stageCount), 0, stageCount - 1);

        return plantData.plantSprites[stageIndex];
    }

}
