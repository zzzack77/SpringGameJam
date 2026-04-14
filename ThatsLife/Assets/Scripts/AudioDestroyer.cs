using System.Collections;
using UnityEngine;

public class AudioDestroyer : MonoBehaviour
{
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyAudio());
    }

    
    private IEnumerator DestroyAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        AudioManager.instance.RemoveAudio(gameObject);
        Destroy(gameObject);
    }

}
