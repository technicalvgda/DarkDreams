﻿using UnityEngine;
using System.Collections;

public class AudioHandlerScript : MonoBehaviour {

    public AudioSource music, sfx, voice;
    public float musicVolume, sfxVolume, voiceVolume;

    public AudioClip[] musicClips;
    public AudioClip[] soundClips;
   

    // Use this for initialization

    void Start()
    {
        ////Initialize variables for playerprefs (if they dont exist)
        //if (!PlayerPrefs.HasKey("Brightness"))
        //{
        //    PlayerPrefs.SetFloat("Brightness", 0);
        //}
        //if (!PlayerPrefs.HasKey("Music"))
        //{
        //    PlayerPrefs.SetFloat("Music", 1);
        //}
        //if (!PlayerPrefs.HasKey("SFX"))
        //{
        //    PlayerPrefs.SetFloat("SFX", 1);
        //}
        //if (!PlayerPrefs.HasKey("Voice"))
        //{
        //    PlayerPrefs.SetFloat("Voice", 1);
        //}
        //if (!PlayerPrefs.HasKey("Vibrate"))
        //{
        //    PlayerPrefs.SetFloat("Vibrate", 1);
        //}
        musicVolume = PlayerPrefs.GetFloat("Music");
        sfxVolume = PlayerPrefs.GetFloat("SFX");
        voiceVolume = PlayerPrefs.GetFloat("Voice");
        music.volume = musicVolume;
        sfx.volume = sfxVolume;
        voice.volume = voiceVolume;
    }

    public void PlayMusic(int i)
    {
        if (music.isPlaying)
        {
            music.Stop();
        }
        music.clip = musicClips[i];
        music.Play();

    }
    public void PlaySound(int i)
    {
        if (sfx.isPlaying)
        {
            sfx.Stop();
        }
        sfx.clip = soundClips[i];
        sfx.Play();

    }
    public void PlayVoice(string voiceClip)
    {
        Resources.UnloadUnusedAssets();
        if (voice.isPlaying)
        {
            voice.Stop();
        }
        voice.clip = Resources.Load("VoiceAudio/"+voiceClip) as AudioClip;
        voice.Play();

    }
    public void LoopMusic(bool b)
    {
        music.loop = b;
    }
    public void StopMusic()
    {
        music.Stop();
    }
}
