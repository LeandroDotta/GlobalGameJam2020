using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introController : MonoBehaviour
{

    public AudioSource sfxButtonAS;

    public AudioClip sfxButtonClickAC;
    public AudioClip sfxButtonHoverAC;


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
