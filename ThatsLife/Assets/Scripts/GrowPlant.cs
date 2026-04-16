using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class GrowPlant : MonoBehaviour, IGrowable
{
    private GridManager gridManager;
    private Respawning respawning;

    [SerializeField] public PlantData plantData;

    private SpriteRenderer spriteRenderer;

    private float growthTimer = 0;

    
    private int stageCount;
    private int stageIndex;
    private int currentStageIndex;

    public int StageIndex
    {
        get { return stageIndex; }
    }

    private float respawnTimer = 0f;
    private float respawnInterval = 6f;
    private float respawnIntervalVariation = 2f;
    private float currentRespawnInterval;

    private float deathTimeRandomVariation = 5.0f;

    private float growthChance = 1f;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawning = GetComponent<Respawning>();
        gridManager = FindAnyObjectByType<GridManager>();
        currentRespawnInterval = UnityEngine.Random.Range(respawnInterval - respawnIntervalVariation, respawnInterval + respawnIntervalVariation);
    }

    public void Grow()
    {
        if (stageIndex >= plantData.plantSprites.Length - 1)
        {
            StartCoroutine(Death());
        }
        //Debug.Log(plantData.plantName);

        spriteRenderer.sprite = ChangeSprite();

        respawnTimer += Time.deltaTime;

        if (respawnTimer >= currentRespawnInterval)
        {
            Debug.Log("current respawn interval" + currentRespawnInterval);
            respawnTimer = 0f;
            respawning.Respawn(stageIndex);
            currentRespawnInterval = UnityEngine.Random.Range(respawnInterval - respawnIntervalVariation, respawnInterval + respawnIntervalVariation);
        }
    }

    private Sprite ChangeSprite()
    {
        growthTimer += Time.deltaTime;

        float growthProgress = growthTimer / plantData.growthTime * growthChance;

        stageCount = plantData.plantSprites.Length;
        stageIndex = Mathf.Clamp(Mathf.FloorToInt(growthProgress * stageCount), 0, stageCount - 1);
        currentStageIndex = stageIndex;

        if (currentStageIndex != stageIndex)
        {
            growthChance = UnityEngine.Random.value;
        }

        return plantData.plantSprites[stageIndex];
    }

    

    IEnumerator Death()
    {
        float randomDeathTime = UnityEngine.Random.Range(plantData.deathTime - deathTimeRandomVariation, plantData.deathTime + deathTimeRandomVariation);
        yield return new WaitForSeconds(randomDeathTime);
        gridManager.RemoveObjectFromCell((int)this.transform.position.x, (int)this.transform.position.y);
    }

}
