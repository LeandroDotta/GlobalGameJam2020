using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour

{
    //AudioSource Sources
    public AudioSource sfxAS;
    //sfxAS[0]:pulo1; sfxAS[1]:pulo2; sfxAS[2]:pulo3; sfxAS[3]:tiro1; sfxAS[4]:tiro2; sfxAS[5]:tiro3; 
    public AudioSource musicAS;
    //mucicAS[0]: Musica Tema (Intro); mucicAS[1]: Musica Stage (Fase); mucicAS[2]: Fanfarra Vitória; mucicAS[3]: Fanfarra Game Over

    //AudioClips
    public AudioClip [] sfxAC;
    public AudioClip[] musicAC;

    private static SoundController _instance;

    public static SoundController Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
        //DontDestroyOnLoad(this);
    }

    public void PlaySFX(params int[] sounds)
    {
        int index = Random.Range(0, sounds.Length);
        int soundIndex = sounds[index];

        if (soundIndex >= sfxAC.Length)
            return;

        sfxAS.PlayOneShot(sfxAC[sounds[index]]);
    }

    public void PlaySFX(int soundIndex)
    {
        if (soundIndex >= sfxAC.Length)
            return;

        sfxAS.PlayOneShot(sfxAC[soundIndex]);
    }
}
