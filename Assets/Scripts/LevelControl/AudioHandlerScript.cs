using UnityEngine;
using System.Collections;

public class AudioHandlerScript : MonoBehaviour {

    public AudioSource music, sfx, voice;
    float musicVolume, sfxVolume, voiceVolume;

    public AudioClip[] musicClips;
    public AudioClip[] soundClips;
    public AudioClip[] voiceClips;

    // Use this for initialization
    void Start ()
    {
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
    public void PlayVoice(int i)
    {
        if (voice.isPlaying)
        {
            voice.Stop();
        }
        voice.clip = voiceClips[i];
        voice.Play();

    }
    public void LoopMusic(bool b)
    {
        music.loop = b;
    }
}
