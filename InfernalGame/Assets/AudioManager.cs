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

    public AudioClip playerHurt;
    public AudioClip swordSwing;
    public AudioClip skeletonHurt;
    public AudioClip skeletonDeath;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main Menu":
            case "Level Select":
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

    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public enum SFX
    {
        PLAYER_HURT,
        SWORD_SWING,
        SKELETON_HURT,
        SKELETON_DEATH,
    }

    public void PlaySFX(SFX sfx)
    {
        switch (sfx)
        {
            case SFX.PLAYER_HURT:
                sfxSource.PlayOneShot(playerHurt);
                break;
            case SFX.SWORD_SWING:
                sfxSource.PlayOneShot(swordSwing);
                break;
            case SFX.SKELETON_HURT:
                sfxSource.PlayOneShot(skeletonHurt);
                break;
            case SFX.SKELETON_DEATH:
                sfxSource.PlayOneShot(skeletonDeath);
                break;
        }
    }
}
