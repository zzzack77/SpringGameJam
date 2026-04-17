using UnityEngine;

public class Plant : MonoBehaviour
{
    private IGrowable growPlant;
    private IHarvestable harvestPlant;

    private void Awake()
    {
        growPlant = GetComponent<IGrowable>();
        harvestPlant = GetComponent<IHarvestable>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        growPlant.Grow();
    }
}
