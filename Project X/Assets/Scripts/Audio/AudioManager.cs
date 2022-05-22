using UnityEngine;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<Sound> sounds = new List<Sound>();


    private void Awake() {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }        
    }

    private void Start() {
        Play("ThemeSong");
    }

    public void Play(string audioName)
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name.Equals(audioName))
            {
                sound.source.Play();
            }
        }
    }
}
