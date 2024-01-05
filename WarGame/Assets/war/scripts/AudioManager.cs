using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Menu.AudioValue;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Menu.AudioValue;
    }
}
