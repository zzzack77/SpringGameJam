using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Scriptable Objects/PlantData")]
public class PlantData : ScriptableObject
{
    [Header("Info")]
    public string plantName;
    public Sprite[] plantSprites;
    public int seedPrice = 4;

    [Header("Growth")]
    public float growthTime = 30f;
    public bool needsWater = false;
    public bool regrows = false;

    [Header("Harvest")]
    public int harvestPrice = 10;
    public int worseHarvestPrice = 7;



    public int harvestIndex = 5;
    public int worseHarvestIndex = 6;
}
