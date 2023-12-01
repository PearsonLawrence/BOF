using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    background,
    powerup,
    shoot,
    bossShoot,
    takeDamage,
    playerMelee,
    enemyMelee
}

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")] 
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip powerup;
    public AudioClip shoot;
    public AudioClip bossShoot;
    public AudioClip takeDamage;
    public AudioClip playerMelee;
    public AudioClip enemyMelee;

    //to add SFX
    //create AudioManager object
    //call PlaySFX with correct sound clip
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(SoundType type)
    {
        
        switch (type)
        {
            case SoundType.takeDamage:
                SFXSource.PlayOneShot(takeDamage);
                break;
            case SoundType.powerup:
                SFXSource.PlayOneShot(powerup);
                break;
            case SoundType.playerMelee:
                SFXSource.PlayOneShot(playerMelee);
                break;
            case SoundType.enemyMelee:
                SFXSource.PlayOneShot(enemyMelee);
                break;
            case SoundType.shoot:
                SFXSource.PlayOneShot(shoot);
                break;
            case SoundType.bossShoot:
                SFXSource.PlayOneShot(bossShoot);
                break;
        }
    }
}
