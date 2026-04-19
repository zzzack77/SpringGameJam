using System.Collections;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private TimeManager timeManager;

    [SerializeField] private GameObject rainAudio;
    //[SerializeField] private GameObject rainGameObject;
    [SerializeField] private ParticleSystem rainParticles;

    private bool rainActive = false;

    // used to set in inspector
    [SerializeField] private bool setRainOn;

    [SerializeField] private GameObject sunPivot;
    [SerializeField] private GameObject sun;

    private float sunnyTime = 180;
    private float sunnyTimeVariation = 30;
    private float rainTime = 30;
    private float rainTimeVariation = 15;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        StartCoroutine(TurnOnRainCycle());
        //AudioManager.instance.SpawnAudio(rainAudio);


    }

    // Update is called once per frame
    void Update()
    {
        if (rainActive != setRainOn)
        {
            rainActive = setRainOn;
            UpdateRain(rainActive);
        }
        UpdateSunPos();
    }

    IEnumerator TurnOnRainCycle()
    {
        UpdateRain(false);
        yield return new WaitForSeconds(Random.Range(sunnyTime - sunnyTimeVariation, sunnyTime + sunnyTimeVariation));
        UpdateRain(true);
        yield return new WaitForSeconds(Random.Range(rainTime - rainTimeVariation, rainTime + rainTimeVariation));
        
        StartCoroutine(TurnOnRainCycle());
    }
    private void UpdateSunPos()
    {
        float t = (timeManager.TimeOfDay / timeManager.DayLength);

        sunPivot.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 180, t));
    }

    // set true to turn rain on, false to turn off
    public void UpdateRain(bool rainBool)
    {
        rainActive = rainBool;
        setRainOn = rainBool;
        //rainGameObject.SetActive(rainBool);
        if (rainBool) rainParticles.Play();
        else rainParticles.Stop();
        if (rainBool)
        {
            //AudioManager.instance.SpawnAudio(rainAudio);
        }
        else
        {
            // Destroy audio (prefereable fade out)
        }
    }
}
