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
    // How many days have passed, starts on 79 as that is the start of spring
    private float totalDays;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSeason = Season.Spring;
        totalGameTime = dayLength * 79;
    }

    // Update is called once per frame
    void Update()
    {
        totalGameTime += Time.deltaTime;
        totalDays = totalGameTime / dayLength;
        int dayNumber = Mathf.FloorToInt(totalDays);
        Debug.Log(dayNumber);
    }
}
