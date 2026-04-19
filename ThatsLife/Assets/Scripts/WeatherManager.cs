using System.Collections;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    private TimeManager timeManager;

    [SerializeField] private GameObject rainAudio;
    private GameObject rainGameObject;
    [SerializeField] private ParticleSystem rainParticles;

    [SerializeField] private GameObject sunPivot;
    [SerializeField] private GameObject sun;

    private float sunnyTime = 120;
    private float sunnyTimeVariation = 30;
    private float rainTime = 30;
    private float rainTimeVariation = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        StartCoroutine(TurnOnRainCycle());
    }

    // Update is called once per frame
    void Update()
    {
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

        if (rainBool)
        {
            rainParticles.Play();
            AudioManager.instance.SpawnAudioWithOut(rainAudio, out rainGameObject);
        }
        else if (rainGameObject != null)
        {
            rainParticles.Stop();
            StartCoroutine(FadeRainOut());
        }
        else
        {
            rainParticles.Stop();
        }
    }
    IEnumerator FadeRainOut()
    {
        AudioSource rainGeeza = rainGameObject.GetComponent<AudioSource>();
        rainGeeza.volume -= 0.05f;
        yield return new WaitForSeconds(0.1f);
        if (rainGeeza.volume > 0) { StartCoroutine(FadeRainOut()); }
        else
        {
            AudioManager.instance.RemoveAudio(rainGameObject);
            Destroy(rainGameObject);
            yield return null;
        }
    }
}
