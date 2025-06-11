using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    public  Vector2 direction;
    public Rigidbody2D rb;
    void Start()
    {
        
        var angle=Random.Range(0,360);
        direction = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad),Mathf.Sin(angle*Mathf.Deg2Rad));
        rb.velocity = direction;
      
    }

    private void Update()
    {
     Helper.xunhuan(this.transform);
    }

    public bool IsBig=true;
    public void BigSize()//大石头
    {
        IsBig=true;
        GetComponent<CircleCollider2D>().radius = 0.144f;
        transform.Find("BigRock").gameObject.SetActive(true);
        //Resources.Load("Smallock");
        transform.Find("SmallRock").gameObject.SetActive(false);
        
    }

    public void SmallSize()//小石头
    {
        IsBig=false;
        GetComponent<CircleCollider2D>().radius = 0.06f; 
        transform.Find("BigRock").gameObject.SetActive(false);
        //Resources.Load("SmallRock");
        transform.Find("SmallRock").gameObject.SetActive(true);
    }
   
    
}
