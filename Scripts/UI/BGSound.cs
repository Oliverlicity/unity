using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using eventsystem = UnityEngine.EventSystems.EventSystem;
public class BGSound : MonoBehaviour
{
    public Button button;
    private Image image;
    public GameObject background,AudioSound;
    public Image Return;
    private void Start()
    {
        image =background.GetComponent<Image>();
        image.gameObject.SetActive(false);
        
        Return.gameObject.SetActive(false);
        
          gameObject.SetActive(false);
          AudioSound.SetActive(false);
            button.onClick.AddListener(
               ()=>
                sound()
                );
        
    }


    public void sound()
    {
        gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        
        Return.gameObject.SetActive(true);
        
        AudioSound.SetActive(true);
        image.DOFade(1f, 1f);
    }

}
