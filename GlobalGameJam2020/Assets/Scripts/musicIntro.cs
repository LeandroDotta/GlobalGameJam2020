using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicIntro : MonoBehaviour
{

    public AudioSource musicAS;

    // Start is called before the first frame update
    void Start()
    {
        musicAS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
