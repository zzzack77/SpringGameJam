using UnityEngine;

public class GrowPlant : MonoBehaviour, IGrowable
{
    [SerializeField] private PlantData plantData;

    private SpriteRenderer spriteRenderer;

    private float growthTimer = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Grow()
    {
       

        spriteRenderer.sprite = ChangeSprite();
    }

    private Sprite ChangeSprite()
    {
        growthTimer += Time.deltaTime;

        float growthProgress = growthTimer / plantData.growthTime;

        int stageCount = plantData.plantSprites.Length;
        int stageIndex = Mathf.Clamp(Mathf.FloorToInt(growthProgress * stageCount), 0, stageCount - 1);

        return plantData.plantSprites[stageIndex];
    }

}
