using System.Collections;
using UnityEngine;

public class MusicFader : MonoBehaviour
{
    private AudioSource musicSource;

    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = 0f;
    }

    private void Start()
    {
        StartCoroutine(FadeMusicIn());
    }


    private IEnumerator FadeMusicIn()
    {
        while (musicSource.volume < 1f)
        {
            musicSource.volume += AudioManager.instance.musicFadeRate * Time.deltaTime;
            yield return null;
        }

        musicSource.volume = 1f;
    }
}
