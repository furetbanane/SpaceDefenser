using UnityEngine;

public class musicManager : MonoBehaviour
{
 
    public AudioClip[] playList;
    public AudioSource audioSource;
    int musicIndex = 0;
    void Start()
    {
        audioSource.clip = playList[0];
        audioSource.Play();
    }


    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playList.Length;
        audioSource.clip = playList[musicIndex];
        audioSource.Play();
    }
}
