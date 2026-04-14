using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private List<GameObject> soundPrefabs = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Use this to spawn in a sound prefab
    public void SpawnAudio(GameObject soundPrefab)
    {
        GameObject sound = Instantiate(soundPrefab, transform.position, Quaternion.identity);

        soundPrefabs.Add(sound);
    }

    public void RemoveAudio(GameObject soundPrefab)
    {
        if (soundPrefabs.Contains(soundPrefab))
        {
            soundPrefabs.Remove(soundPrefab);
        }
    }

    // Use this to spanwn a random prefab from an array
    public void SpawnRandomAudio(GameObject[] randomSounds)
    {
        int randIndex = Random.Range(0, randomSounds.Length - 1);
        GameObject sound = Instantiate(randomSounds[randIndex], transform.position, Quaternion.identity);

        soundPrefabs.Add(sound);
    }

 
}
