using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    protected static SoundManager instance;
    public static SoundManager Instance => instance;

    private string currentPlayZone = "";
    //public Sound sound;

    public List<Sound> sounds = new List<Sound>();
    public List<Sound> sfxs = new List<Sound>();
    private Dictionary<string, AudioSource> musicSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> sfxSources = new Dictionary<string, AudioSource>();
    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
            return;
        }
        //create musicSoure and add to Dictionary
        foreach (Sound sound in sounds)
        {
            AudioSource musicAudioSource = gameObject.AddComponent<AudioSource>();
            musicAudioSource.clip = sound.clip;
            musicAudioSource.volume = sound.volume;
            musicAudioSource.loop = sound.isLoop;
            musicSources.Add(sound.name, musicAudioSource);
        }

        //create sfxSoure and add to Dictionary
        foreach (Sound sfx in sfxs)
        {
            AudioSource SfxAudioSource = gameObject.AddComponent<AudioSource>();
            SfxAudioSource.clip = sfx.clip;
            SfxAudioSource.volume = sfx.volume;
            SfxAudioSource.loop = sfx.isLoop;
            sfxSources.Add(sfx.name, SfxAudioSource);
        }
    }

    
    public void PlayMusic(string name, string triggerZone)
    {
        if(musicSources.ContainsKey(name))
        {
            musicSources[name].Play();
            currentPlayZone = triggerZone;
        }
        else
        {
            Debug.LogWarning("music " +name + " not found!");
        }

    }

    public void PlaySfx(string name)
    {
        if (sfxSources.ContainsKey(name))
        {
            sfxSources[name].Play();
        }
        else
        {
            Debug.LogWarning("sfx " + name + " not found!");
        }

    }

    public void StopMusic(string name, string triggerZone)
    {
        if (musicSources.ContainsKey(name) && currentPlayZone == triggerZone)
        {
            musicSources[name].Stop();
            currentPlayZone = "";
        }
        else
        {
            return;
        }
    }

    public void StopAllSound()
    {
        foreach (var music in musicSources.Values)
        {
            music.Stop();
        }

        foreach (var sfx in sfxSources.Values)
        {
            sfx.Stop();
        }
    }    
    public float GetVolumeMusicSources(string name)
    {
        if(musicSources.ContainsKey(name))
        {
            AudioSource musicSource = musicSources[name];
            return musicSource.volume;
        }
        else
        {
            Debug.LogWarning("Music " + name + " not found!");
            return 0f;
        }    
    }

    public float GetVolumeSfxSources(string name)
    {
        if (sfxSources.ContainsKey(name))
        {
            AudioSource sfxSource = sfxSources[name];
            return sfxSource.volume;
        }
        else
        {
            Debug.LogWarning("Sfx " + name + " not found!");
            return 0f;
        }
    }
    public void MusicVolume(float volume)
    {
        foreach (var musicVolume in musicSources.Values)
        {
            musicVolume.volume = volume;
        }
    }

    public void SfxVolume(float volume)
    {
        foreach (var SfxVolume in sfxSources.Values)
        {
            SfxVolume.volume = volume;
        }
    }
}
