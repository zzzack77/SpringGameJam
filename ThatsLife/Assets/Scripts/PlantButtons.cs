using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantButtons : MonoBehaviour
{
    [SerializeField] private PlantData plantData;

    [SerializeField] private Image plantImage;
    [SerializeField] private TextMeshProUGUI plantPriceText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plantImage.sprite = plantData.plantSprites[4]; // Shows fully grown plant
        plantPriceText.text = "$" + plantData.seedPrice.ToString();
    }
}
