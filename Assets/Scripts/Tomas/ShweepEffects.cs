using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShweepEffects : MonoBehaviour {

    public AudioClip clip;
    public ParticleSystem effect;

    public void OnShweep()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        effect.Play();
    }
}
