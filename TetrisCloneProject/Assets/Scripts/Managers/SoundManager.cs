using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public bool musicEnabled = true;
    public bool fxEnabled = true;

    [Range(0, 1)]
    public float musicVolume = 1.0f;

    [Range(0, 1)]
    public float fxVolume = 1.0f;

    public AudioClip clearRowSound;
    public AudioClip moveSound;
    public AudioClip dropSound;
    public AudioClip gameOverSound;

    public AudioSource musicSource;

    // Background music clips
    public AudioClip[] musicClips;

    AudioClip randomMusicClip;

    // Use this for initialization
    void Start()
    {
        this.randomMusicClip = GetRandomClip(this.musicClips);
        PlayBackgroundMusic(this.randomMusicClip);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public AudioClip GetRandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }

    public void PlayBackgroundMusic(AudioClip musicClip)
    {
        // Return if music is disabled or if musicSource is null or is musicClip is null
        if (!this.musicEnabled || !musicClip || !this.musicSource)
        {
            return;
        }

        // If music is playing, stop it
        this.musicSource.Stop();

        this.musicSource.clip = musicClip;

        // Set the music volume
        this.musicSource.volume = musicVolume;

        // Music repeats forever
        this.musicSource.loop = true;

        // Start playing
        this.musicSource.Play();
    }

    void UpdateMusic()
    {
        if (this.musicSource.isPlaying != musicEnabled)
        {
            if (this.musicEnabled)
            {
                this.randomMusicClip = GetRandomClip(this.musicClips);
                PlayBackgroundMusic(this.randomMusicClip);
            }
            else
            {
                this.musicSource.Stop();
            }
        }
    }

    public void ToggleMusic()
    {
        this.musicEnabled = !this.musicEnabled;
        UpdateMusic();
    }
}
