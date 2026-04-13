using UnityEngine;

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}
public class TimeManager : MonoBehaviour
{
    public Season currentSeason;
    // Day Length is how many seconds each day will be
    private float dayLength = 15f;
    private float totalGameTime;
    int lastDayNumber = -1; // start at -1 so day 1 prints immediately
    // How many days have passed, starts on 79 as that is the start of spring
    private float totalDays;

    [SerializeField] private float startingDay = 50;

    private float currentYear = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSeason = Season.Spring;
        totalGameTime = dayLength * startingDay; // Sets the game time to be in line with the starting day
    }

    // Update is called once per frame
    void Update()
    {
        totalGameTime += Time.deltaTime;
        totalDays = totalGameTime / dayLength;
        int dayNumber = Mathf.FloorToInt(totalDays);
        

        if (dayNumber != lastDayNumber)
        {
            lastDayNumber = dayNumber;
            Debug.Log("New Day: " + dayNumber);
            
            // Check to see what season we are in based on the day
            if (dayNumber >= 0 && dayNumber < 79 || dayNumber > 335) { currentSeason = Season.Winter; } // Winter
            else if (dayNumber >= 79 && dayNumber < 152) { currentSeason = Season.Spring; } // Spring
            else if (dayNumber >= 152 && dayNumber < 243) { currentSeason = Season.Summer; } // Summer
            else if (dayNumber >= 243 && dayNumber < 335) { currentSeason = Season.Autumn; } // Autumn
            
            if (dayNumber == 366) 
            { 
                totalGameTime = 0;
                currentYear += 1;
              
                print("New Year");
            }

                Debug.Log("Season: " + currentSeason.ToString());
        }

       
    }
}
