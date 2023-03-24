using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] AudioSource sfxPlayer;

    [SerializeField] float fadeDuration;
    float originaMusic;

    public static AudioController i { get; private set; }

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        originaMusic = musicPlayer.volume;
    }

    public void PlayMusic(AudioClip audio, bool loop=true, bool fade=false)
    {
        if (audio == null)
            return;

        StartCoroutine(PlayMusicAsync(audio, loop, fade));
    }

    IEnumerator PlayMusicAsync(AudioClip audio, bool loop = true, bool fade = false)
    {
        if (fade)
        {
            yield return musicPlayer.DOFade(0, fadeDuration).WaitForCompletion();
        }
        musicPlayer.clip = audio;
        musicPlayer.loop = loop;
        musicPlayer.Play();

        if (fade)
        {
            yield return musicPlayer.DOFade(originaMusic, fadeDuration).WaitForCompletion();
        }
    }
}
