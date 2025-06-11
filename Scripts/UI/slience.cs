using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class slience : MonoBehaviour
{
    private Button buttonSound;
    private Transform bgsound;
    private AudioSource s;
    void Start()
    {
        //var Bgsound = Resources.Load("Sound/03 Descent");
        //var BgSound = Bgsound.GetComponent<AudioSource>();
         bgsound=transform.Find("slience");
         s =bgsound.GetComponent<AudioSource>();
        buttonSound = GetComponent<Button>();
        buttonSound.onClick.AddListener(
            () =>
                BGsound()
        );
    }

    void BGsound()
    {
        if (s.volume == 0f)
        {
            buttonSound = GetComponent<Button>();
            buttonSound.onClick.AddListener(
                ()=>
                    s.volume = 1f
            );
        }

        if (s.volume == 1f)
        {
            buttonSound = GetComponent<Button>();
            buttonSound.onClick.AddListener(
                ()=>
                    s.volume = 0f
            );
        }
    }
    
}
