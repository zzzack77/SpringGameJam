using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private GameObject rain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.SpawnAudio(rain);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("RAIN NO MORE");
            AudioManager.instance.RemoveAudio(rain);
        }
    }
}
