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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
