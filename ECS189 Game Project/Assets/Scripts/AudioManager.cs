using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Initialize sounds
    void Awake()
    {
        // Make sure there's only one instance of the audio in the scene
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        foreach (Sound s in this.sounds)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("GameplayMusic");
    }

    public void Play(string name)
    {
        // Find the sound where sound.name is equal to "name"
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
            s.source.Play();
    }
}
