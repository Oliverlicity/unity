using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class Return : MonoBehaviour
{
    private Button button;
    public GameObject background,AudioSound,panel;
    private Image image,Panel;
    void Start()
    {
        image = background.GetComponent<Image>();
        Panel =panel.GetComponent<Image>();
        
        button = GetComponent<Button>();
          button.onClick.AddListener(
              ()=>
                  fade()
              );
    }
    
    public void fade()
    {
        gameObject.SetActive(false);
        
        AudioSound.SetActive(false);
        image.DOFade(0, 1f);
        image.gameObject.SetActive(true);
        Panel.gameObject.SetActive(false);
    }

    private void Update()
    {
         button.onClick.AddListener(
             ()=>
                 fade()
         );
    }
    
}
