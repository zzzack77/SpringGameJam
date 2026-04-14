using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI seasonText;
    [SerializeField] private TextMeshProUGUI yearText;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        TimeManager.OnDayChanged += UpdateDayText;
        TimeManager.OnSeasonChanged += UpdateSeasonText;
        TimeManager.OnYearChanged += UpdateYearText;
        MoneyManager.OnMoneyUpdated += UpdateMoneyText;
    }

    private void OnDisable()
    {
        TimeManager.OnDayChanged -= UpdateDayText;
        TimeManager.OnSeasonChanged -= UpdateSeasonText;
        TimeManager.OnYearChanged -= UpdateYearText;
        MoneyManager.OnMoneyUpdated -= UpdateMoneyText;
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
}
