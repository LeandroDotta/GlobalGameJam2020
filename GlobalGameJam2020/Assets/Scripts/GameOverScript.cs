using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public AudioSource sfxButtonAS;

    public AudioClip sfxButtonClickAC;
    public AudioClip sfxButtonHoverAC;

    public AudioClip sfxPlayerGameOver;



    // Start is called before the first frame update
    void Start()
    {
        sfxButtonAS.PlayOneShot(sfxPlayerGameOver);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void carregaCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }



    public void PlaySfxButtonClick()
    {
        sfxButtonAS.PlayOneShot(sfxButtonClickAC);
    }

    public void PlaySfxButtonHover()
    {
        sfxButtonAS.PlayOneShot(sfxButtonHoverAC);
    }
}