using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip sliceClip;
    [SerializeField] private AudioClip explodeClip;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (backgroundMusic == null || musicSource == null) return;
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySlice()
    {
        if (sliceClip != null && sfxSource != null)
            sfxSource.PlayOneShot(sliceClip);
    }

    public void PlayExplode()
    {
        if (explodeClip != null && sfxSource != null)
            sfxSource.PlayOneShot(explodeClip);
    }

    /// <summary>
    /// sliderValue is expected 0…1; we convert to –80…0 dB
    /// </summary>
    public void SetMusicVolume(float sliderValue)
    {
        float dB = Mathf.Lerp(-80f, 0f, sliderValue);
        audioMixer.SetFloat("MusicVolume", dB);
    }

    public void SetSfxVolume(float sliderValue)
    {
        float dB = Mathf.Lerp(-80f, 0f, sliderValue);
        audioMixer.SetFloat("SFXVolume", dB);
    }
}
