using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIPowerUp : MonoBehaviour
{
    public static UIPowerUp powerUp;
    private void Awake()
    {
        powerUp = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

   
   
}
