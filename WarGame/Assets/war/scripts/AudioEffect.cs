using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    AudioSource source;


    public AudioClip clip;  
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Playeff()
    {
        source.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
