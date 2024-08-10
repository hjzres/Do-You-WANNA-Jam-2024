using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{

    [Serializable]
    public class Sound
    {
        public string Name;
        public AudioClip Clip;

        [Range(0.0f, 1.0f)] public float Volume = 1;
        [Range(0.1f, 3.0f)] public float Pitch = 1;
        public bool Loop;

        [HideInInspector] public AudioSource Source;
    }

    [SerializeField] private List<Sound> soundtracks;
    [SerializeField] private List<Sound> soundEffects;
    public string CurrentSoundtrack;

    protected override void Awake()
    {
        base.Awake();

        foreach (Sound sound in soundEffects){
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }

        foreach (Sound sound in soundtracks){
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    public void PlaySoundtracks (string newSoundtrack)
    {
        Sound soundtrack = soundtracks.Find(track => track.Name == newSoundtrack);

        //Stop the same soundtrack from restarting itself
        if (soundtrack == null || CurrentSoundtrack == newSoundtrack)
            return;

        foreach (Sound track in soundtracks)
            track.Source.Stop();

        //Play the soundtrack
        soundtrack.Source.Play();

        //Reassign the current soundtrack to the new soundtrack
        CurrentSoundtrack = newSoundtrack;
    }

    public void PlaySoundEffect (string newSoundEffect)
    {
        List<Sound> effects = soundEffects.FindAll(effect => effect.Name.Contains(newSoundEffect));
        int randomIndex = 0;
        Sound soundEffect;

        if(effects.Count > 1) {
            randomIndex = UnityEngine.Random.Range(0, effects.Count);
            soundEffect = effects[randomIndex];
        } else {
            return;
        }

        //Play the sound effect
        soundEffect.Source.Play();
    }
}
