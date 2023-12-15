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
    [SerializeField] AudioSource musicSource2;
    [SerializeField] AudioSource SFXSource;
    public StarBoss boss;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip bossBackground;
    public AudioClip powerup;
    public AudioClip shoot;
    public AudioClip bossShoot;
    public AudioClip takeDamage;
    public AudioClip playerMelee;
    public AudioClip enemyMelee;

    public bool doBossFight;

    public bool bossDead;
    //to add SFX
    //create AudioManager object
    //call PlaySFX with correct sound clip
    private void Start()
    {
    }
    public void Update()
    {
        if (bossDead)
        {
            if (musicSource.volume > .01f)
                musicSource.volume -= Time.deltaTime * 2;

            if (musicSource2.volume > .01f)
                musicSource2.volume -= Time.deltaTime * 2;
        }
        else if (boss.isFighting)
        {
           
                Debug.Log("Fighting");
                musicSource.volume -= Time.deltaTime * .5f;
                if (musicSource.volume < .2f && !musicSource2.isPlaying)
                {
                    musicSource2.PlayOneShot(bossBackground);
                }

                if (musicSource2.isPlaying && musicSource2.volume != 75)
                {
                    musicSource2.volume += Time.deltaTime * .5f;
                }

            
           
        }
        else
        {
            Debug.Log("No Fight");
            if (musicSource.isPlaying == false)
            {
                musicSource.PlayOneShot(background);

            }
        }
    }
   
}
