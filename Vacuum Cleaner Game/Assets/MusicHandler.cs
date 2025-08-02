using System.Collections;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{

    public static MusicHandler instance;

    [SerializeField]
    AudioSource[] musicSources;

    private void Start()
    {
        instance = this;


        foreach (AudioSource source in musicSources) { source.volume = 0; source.loop = true; }

        PlaySong(0);
    }

    public void PlaySong(int sourceID)
    {
        AudioSource source = musicSources[sourceID];


        foreach (var track in musicSources)
        {
            if (track != source)
            {
                StartCoroutine(FadeOut(track, 3));
            }
            else
            {
                source.Play();
                StartCoroutine(FadeIn(source, 6));
            }
        }
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0, time / duration);
            yield return null;
        }

        source.volume = 0f;
        source.Stop(); // Optional: stop playback completely
    }

    private IEnumerator FadeIn(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 1, time / duration);
            yield return null;
        }

        source.volume = 1f;
    }

}
