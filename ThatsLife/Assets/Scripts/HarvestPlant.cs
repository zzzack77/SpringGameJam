using UnityEngine;

public class HarvestPlant : MonoBehaviour, IHarvestable
{

    [SerializeField] private PlantData plantData;
    [SerializeField] private MoneyManager monManager;
    private Plant plant;

    private void Awake()
    {
        plant = GetComponent<Plant>();
        monManager = FindAnyObjectByType<MoneyManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Harvest()
    {
        // Check to see if plant is in right growth stage
        monManager.AddMoney(plantData.plantPrice);
    }

}
