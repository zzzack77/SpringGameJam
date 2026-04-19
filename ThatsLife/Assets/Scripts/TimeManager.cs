using System;
using System.Security.Cryptography;
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
    public static event Action<int> OnDayChanged;
    public static event Action<Season> OnSeasonChanged;
    public static event Action<int> OnYearChanged;

    public static Action PauseGame;
    public static Action PlayGame;

    public Season currentSeason;
    private Season trackedSeason;
    // Day Length is how many seconds each day will be
    private float dayLength = 15f;
    private float totalGameTime;
    int lastDayNumber = -1; // start at -1 so day 1 prints immediately
    // How many days have passed, starts on 79 as that is the start of spring
    private float totalDays;

    private float currentTimeOfDay;
    public float TimeOfDay
    {
        get { return currentTimeOfDay; }
    }
    public float DayLength
    {
        get { return dayLength; }
    }

    [SerializeField] private float startingDay = 0;

    private int currentYear = 1;
    [SerializeField] private GameObject mainMenuMusicLoop;
    [SerializeField] private GameObject winterMusicLoop;
    [SerializeField] private GameObject springMusicLoop;
    [SerializeField] private GameObject summerMusicLoop;
    [SerializeField] private GameObject autumnMusicLoop;

    [SerializeField] private float timeScaler = 1f;

    private void OnEnable()
    {
        PauseGame += OnGamePause;
        PlayGame += OnGamePlay;
    }

    private void OnDisable()
    {
        PauseGame -= OnGamePause;
        PlayGame -= OnGamePlay;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        trackedSeason = currentSeason;

        

        totalGameTime = dayLength * startingDay; // Sets the game time to be in line with the starting day

        OnSeasonChanged?.Invoke(currentSeason);
        OnYearChanged?.Invoke(currentYear);


        AudioManager.instance.SpawnMusic(mainMenuMusicLoop);
    }

    // Update is called once per frame
    void Update()
    {
        totalGameTime += Time.deltaTime;
        currentTimeOfDay += Time.deltaTime;
        
        totalDays = totalGameTime / dayLength;
        int dayNumber = Mathf.FloorToInt(totalDays);
        

        if (dayNumber != lastDayNumber)
        {
            lastDayNumber = dayNumber;
            OnDayChanged?.Invoke(dayNumber);

            currentTimeOfDay = 0f;
            // Check to see what season we are in based on the day
            if (dayNumber >= 0 && dayNumber < 26 || dayNumber > 100) { currentSeason = Season.Winter; } // Winter
            else if (dayNumber >= 26 && dayNumber < 51) { currentSeason = Season.Spring; } // Spring
            else if (dayNumber >= 51 && dayNumber < 76) { currentSeason = Season.Summer; } // Summer
            else if (dayNumber >= 76 && dayNumber < 101) { currentSeason = Season.Autumn; } // Autumn
            
            if (dayNumber == 101) 
            { 
                totalGameTime = 0;
                currentYear += 1;

                OnYearChanged?.Invoke(currentYear);
            }

            if (trackedSeason != currentSeason)
            {
                OnSeasonChanged?.Invoke(currentSeason);


                SetSeasonMusic();

                
                trackedSeason = currentSeason;
            }
            
        }

       
    }

    private void SetSeasonMusic()
    {
        // Change Music
        switch (currentSeason)
        {
            case Season.Winter:
                AudioManager.instance.SpawnMusic(winterMusicLoop);
                break;

            case Season.Spring:
                AudioManager.instance.SpawnMusic(springMusicLoop);
                break;

            case Season.Summer:
                AudioManager.instance.SpawnMusic(summerMusicLoop);
                break;

            case Season.Autumn:
                AudioManager.instance.SpawnMusic(autumnMusicLoop);
                break;
        }
    }

    private void OnGamePlay()
    {
        Time.timeScale = timeScaler;
        SetSeasonMusic();
    }

    private void OnGamePause()
    {
        Time.timeScale = 0f;
    }
}
