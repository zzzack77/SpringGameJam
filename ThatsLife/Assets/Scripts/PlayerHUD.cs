using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI seasonText;
    [SerializeField] private TextMeshProUGUI yearText;
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Season Image")]
    [SerializeField] private Image seasonImage;
    [SerializeField] private Sprite winterImage;
    [SerializeField] private Sprite springImage;
    [SerializeField] private Sprite summerImage;
    [SerializeField] private Sprite autumnImage;


    private void OnEnable()
    {
        TimeManager.OnDayChanged += UpdateDayText;
        TimeManager.OnSeasonChanged += UpdateSeasonText;
        TimeManager.OnYearChanged += UpdateYearText;
        MoneyManager.OnMoneyUpdated += UpdateMoneyText;

        TimeManager.OnSeasonChanged += UpdateSeasonImage;
    }

    private void OnDisable()
    {
        TimeManager.OnDayChanged -= UpdateDayText;
        TimeManager.OnSeasonChanged -= UpdateSeasonText;
        TimeManager.OnYearChanged -= UpdateYearText;
        MoneyManager.OnMoneyUpdated -= UpdateMoneyText;

        TimeManager.OnSeasonChanged -= UpdateSeasonImage;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateDayText(int currentDay)
    {
        dayText.text = "Day: " + currentDay.ToString();
    }

    private void UpdateSeasonText(Season currentSeason)
    {
        seasonText.text = currentSeason.ToString();
    }

    private void UpdateYearText(int currentYear)
    {
        yearText.text = "Year: " + currentYear.ToString();
    }

    private void UpdateMoneyText(int moneyAmount)
    {
        moneyText.text = "$" + moneyAmount.ToString();
    }

    private void UpdateSeasonImage(Season currentSeason)
    {
        if (seasonImage != null)
        {
            switch (currentSeason)
            {
                case Season.Winter:
                    seasonImage.sprite = winterImage;
                    break;
                case Season.Spring:
                    seasonImage.sprite = springImage;
                    break;
                case Season.Summer:
                    seasonImage.sprite = summerImage;
                    break;
                case Season.Autumn:
                    seasonImage.sprite = autumnImage;
                    break;
            }
        }
        
    }
}
