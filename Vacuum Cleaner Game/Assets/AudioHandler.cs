using UnityEngine;

public class AudioHandler : MonoBehaviour
{

    public static AudioHandler instance;

    [SerializeField]
    AudioSource[] audioSources;

    void Awake()
    {
        instance = this;
    }

    public void PlaySound(int id)
    {
        audioSources[id].Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
