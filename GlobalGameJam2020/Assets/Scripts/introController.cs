using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introController : MonoBehaviour
{

    public AudioSource sfxButtonAS;

    public AudioClip sfxButtonClickAC;
    public AudioClip sfxButtonHoverAC;

    public GameObject panelInstructions;


    public void carregaCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Intro");
        }
    }

    public void PlaySfxButtonClick()
    {
        sfxButtonAS.PlayOneShot(sfxButtonClickAC);
    }

    public void PlaySfxButtonHover()
    {
        sfxButtonAS.PlayOneShot(sfxButtonHoverAC);
    }

    public void painelInstructions()
    {
        panelInstructions.SetActive(true);
    }


}
