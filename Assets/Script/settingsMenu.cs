using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsMenu : MonoBehaviour
{
    public AudioMixer Music;
    public AudioMixer Sound;
    public void music ()
    {
        
    }
    public void setMusic(float music)
    {
        Debug.Log(music);
        Music.SetFloat("music", music);
    }


    public void setsound (float sound)
    {
        Debug.Log(sound);
        Sound.SetFloat("sound", sound);
    }

}
