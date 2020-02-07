using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLevelText : MonoBehaviour
{

    public GameObject panelLevelText;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("desativaPanel");
    }


    IEnumerator desativaPanel ()
    {
        yield return new WaitForSeconds(5f);
        panelLevelText.SetActive(false);
            
    }

}
