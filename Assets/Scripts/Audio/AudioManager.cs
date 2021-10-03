using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] Sounds;
    public static AudioManager Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        //Check if we use it project wide
        //DontDestroyOnLoad(gameObject);
        foreach (Sound s in Sounds) {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            if (s.PlayOnStart == true) {
                Play(s.Name);
            }
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.Source.Play();
    }
}
