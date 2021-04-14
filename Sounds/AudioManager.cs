using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //creates an array called sounds with data from my Sound class
    public Sound[] sounds;

    //reference to this instance of audio manager
    public static AudioManager instance;

    void Awake()
    {
        //ensures there is no more than 1 audio manager
        if( instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // sets the variables of every sound to be those in my Sound script. Also adds the AudioSource component to the game object that calls the sound
        foreach (Sound currentSound in sounds) {
           currentSound.source = gameObject.AddComponent<AudioSource>();
           currentSound.source.clip = currentSound.clip;

           currentSound.source.volume = currentSound.volume;
           currentSound.source.pitch = currentSound.pitch;
           currentSound.source.loop = currentSound.loop;
           currentSound.source.outputAudioMixerGroup = currentSound.masterGroup;

        }
    }

    /*below are methods to be called from other classes. All take a string called "name" to interact with that specific sound after it finds it
      in the array */
    
    public void Play (string name) {
       Sound currentSound = Array.Find(sounds, sound => sound.soundName == name);
       if (currentSound == null) {
           Debug.LogWarning("Sound with name of:" + name + "does not exist");
           return;
       }
       currentSound.source.Play();
    }

    public void Stop (string name) {
        Sound currentSound = Array.Find( sounds, sound => sound.soundName == name);
        if (currentSound != null) {
            currentSound.source.Stop();
        }
    }

    public void Pause (string name) {
        Sound currentSound = Array.Find( sounds, sound => sound.soundName == name);
        if (currentSound == null) {
            Debug.LogWarning("Sound with name of:" + name + "either is not playing, or does not exist");
            return;
        }
        if (currentSound != null) {
            currentSound.source.Pause();
        }
    }

    public void UnPause (string name) {
        Sound currentSound = Array.Find( sounds, sound => sound.soundName == name);
        if (!currentSound.source.isPlaying) {
            currentSound.source.UnPause();
        }
    }

    public void ChangeVolume (string name, float volume) {
        Sound currentSound = Array.Find( sounds, sound => sound.soundName == name);
        currentSound.source.volume = volume;
    }
}
