using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSound : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider slider;
    
    // Update is called once per frame
    void Update()
    {
        audioSource.volume = slider.value;
    }
}
