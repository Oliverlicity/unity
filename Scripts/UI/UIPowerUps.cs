using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerUps : MonoBehaviour
{
   public static UIPowerUps UIPowerups;
   public Image ghost,spread,round,bigger,huge,pass;
   
   private void Awake()
   {
       UIPowerups = this;
   }

   private void Start()
   {
       ghost=transform.Find("ghost").GetComponent<Image>();
       spread =transform.Find("spread").GetComponent<Image>();
       round =transform.Find("round").GetComponent<Image>();   
       bigger =transform.Find("bigger").GetComponent<Image>();  
       huge =transform.Find("huge").GetComponent<Image>();  
       pass =transform.Find("pass").GetComponent<Image>(); 
   }

   
}


