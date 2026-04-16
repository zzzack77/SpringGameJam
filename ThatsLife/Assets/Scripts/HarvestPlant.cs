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

    [SerializeField] private GameObject[] harvestSounds;
    [SerializeField] private GameObject[] badHarvestSounds;
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
            MoneyManager.OnMoneyAdded?.Invoke(plantData.harvestPrice);

            if (harvestSounds != null && harvestSounds.Length > 0) AudioManager.instance.SpawnRandomAudio(harvestSounds);



            GameObject popup = Instantiate(popupText, transform.position, Quaternion.identity, popupCanvas.transform);
            popup.GetComponentInChildren<TextMeshProUGUI>().text = "$" + plantData.harvestPrice.ToString();
        }

        else if (growPlant.StageIndex == plantData.worseHarvestIndex)
        {
            MoneyManager.OnMoneyAdded?.Invoke(plantData.worseHarvestPrice);

            if (harvestSounds != null && harvestSounds.Length > 0) AudioManager.instance.SpawnRandomAudio(harvestSounds);

            GameObject popup = Instantiate(popupText, transform.position, Quaternion.identity, popupCanvas.transform);
            popup.GetComponentInChildren<TextMeshProUGUI>().text = "$" + plantData.worseHarvestPrice.ToString();
        }

        // Play dud harvest sound (duller/ bad sound for harvesting at wrong time)
        if (harvestSounds != null && badHarvestSounds.Length > 0) AudioManager.instance.SpawnRandomAudio(badHarvestSounds);
    }

}
