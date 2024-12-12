using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip mainMenu;
    public AudioClip background;
    public AudioClip win;
    public AudioClip lose;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main Menu":
                PlayMusic(mainMenu, true);
                break;
            case "Win Screen":
                PlayMusic(win);
                break;
            case "Death Screen":
                PlayMusic(lose);
                break;
            default:
                PlayMusic(background, true);
                break;
        }
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }
}
