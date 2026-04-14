using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Scriptable Objects/PlantData")]
public class PlantData : ScriptableObject
{
    [Header("Info")]
    public string plantName;
    public Sprite[] plantSprites;

    [Header("Growth")]
    public float growthTime = 30f;
    public bool needsWater = false;
    public bool regrows = false;

    [Header("Harvest")]
    public int plantPrice = 10;



    public int harvestIndex = 5;
}
