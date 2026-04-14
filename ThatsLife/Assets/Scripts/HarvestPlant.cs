using TMPro;
using UnityEngine;

public class HarvestPlant : MonoBehaviour, IHarvestable
{

    [SerializeField] private PlantData plantData;
    private MoneyManager monManager;
    private Plant plant;
    private GrowPlant growPlant;
    private GameObject popupCanvas;

    [SerializeField] private GameObject popupText;
    private void Awake()
    {
        plant = GetComponent<Plant>();
        growPlant = GetComponent<GrowPlant>();
        monManager = FindAnyObjectByType<MoneyManager>();
        popupCanvas = GameObject.Find("UI Popups");
        
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
        
        if (growPlant.StageIndex == plantData.harvestIndex)
        {
            monManager.AddMoney(plantData.harvestPrice);
            GameObject popup = Instantiate(popupText, transform.position, Quaternion.identity, popupCanvas.transform);
            popup.GetComponentInChildren<TextMeshProUGUI>().text = "$" + plantData.harvestPrice.ToString();
        }

        else if (growPlant.StageIndex == plantData.worseHarvestIndex)
        {
            monManager.AddMoney(plantData.worseHarvestPrice);
            GameObject popup = Instantiate(popupText, transform.position, Quaternion.identity, popupCanvas.transform);
            popup.GetComponentInChildren<TextMeshProUGUI>().text = "$" + plantData.worseHarvestPrice.ToString();
        }


    }

}
