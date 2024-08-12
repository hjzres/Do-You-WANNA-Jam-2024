using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] private List<Soundtrack> soundtracks;
    [SerializeField] private List<SoundEffect> soundEffects;

    [SerializeField] private Soundtrack startingSoundtrack;
    public string CurrentSoundtrack;

    private void Start()
    {

        foreach (Soundtrack soundtrack in soundtracks){
            soundtrack.Source = gameObject.AddComponent<AudioSource>();
            soundtrack.Source.clip = soundtrack.Clip;

            soundtrack.Source.volume = soundtrack.Volume;
            soundtrack.Source.pitch = soundtrack.Pitch;
            soundtrack.Source.loop = soundtrack.Loop;
        }
        
        foreach (SoundEffect soundEffect in soundEffects){
            soundEffect.Source = gameObject.AddComponent<AudioSource>();
            // set the audio source clip when we need to play a sound effect

            soundEffect.Source.volume = soundEffect.Volume;
            soundEffect.Source.pitch = soundEffect.Pitch;
            soundEffect.Source.loop = soundEffect.Loop;
        }

        if(startingSoundtrack != null)
            PlaySoundtrack(startingSoundtrack);
    }

    public void PlaySoundtrack (string newSoundtrack)
    {
        Soundtrack soundtrack = soundtracks.Find(track => track.Name == newSoundtrack);

        //Stop the same soundtrack from restarting itself
        if (soundtrack == null || soundtrack.Source.isPlaying)
            return;

        foreach (Soundtrack track in soundtracks)
            track.Source.Stop();

        //Play the soundtrack
        soundtrack.Source.Play();

        //Reassign the current soundtrack to the new soundtrack
        CurrentSoundtrack = newSoundtrack;
    }

    public void PlaySoundtrack (Soundtrack newSoundtrack)
    {
        //Stop the same soundtrack from restarting itself
        if (newSoundtrack == null || newSoundtrack.Source.isPlaying)
            return;

        foreach (Soundtrack track in soundtracks)
            track.Source.Stop();

        //Play the soundtrack
        newSoundtrack.Source.Play();

        //Reassign the current soundtrack to the new soundtrack
        CurrentSoundtrack = newSoundtrack.Name;
    }

    public void PlaySoundEffect (string newSoundEffect)
    {
        SoundEffect soundEffect = soundEffects.Find(effect => effect.Name == newSoundEffect);

        if(soundEffect == null) 
            return;

        int i = soundEffect.RandomIndex();
        soundEffect.Source.clip = soundEffect.Clips[i];

        //Play the sound effect
        soundEffect.Source.Play();
    }

    public void PlaySoundEffect (SoundEffect newSoundEffect)
    {

        if(newSoundEffect == null) 
            return;

        int i = newSoundEffect.RandomIndex();
        newSoundEffect.Source.clip = newSoundEffect.Clips[i];

        //Play the sound effect
        newSoundEffect.Source.Play();
    }
}
