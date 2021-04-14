using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    //this class is to be used in conjuction with my AudioManager class

    public AudioMixer masterMixer;
    public AudioMixerGroup masterGroup;
    
    //defines variables and referenecs for each audio clip. 
    public string soundName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
