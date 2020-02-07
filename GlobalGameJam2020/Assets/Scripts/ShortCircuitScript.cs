using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCircuitScript : MonoBehaviour
{
    private Hazard hazard;
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        hazard = transform.parent.GetComponent<Hazard>();
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(hazard.on) {
            transform.localScale = new Vector3(Random.Range(0.2f,0.7f), 1.0f, 1.0f);
            ShowLightning(Random.Range(0,4));
            if(!particle.isPlaying) {
                particle.Play();
            }
        } else {
            TurnOffLightning();
            particle.Stop();
        }
    }

    void ShowLightning(int number)
    {
        int x;

        for(x = 0; x < 4; x++) {
            if(x == number)
                transform.GetChild(x).GetComponent<SpriteRenderer>().enabled = true;
            else
                transform.GetChild(x).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void TurnOffLightning() {
        int x;

        for(x = 0; x < 4; x++) {
            transform.GetChild(x).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
