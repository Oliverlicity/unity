using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Helper : MonoBehaviour
    {
        public  static Vector2 LeftBottom =new Vector2(-9.6f,-5.0f);
        public static Vector2 RightTop = new Vector2(9.6f,6.5f);
        public static float ScreenHeight => RightTop.y - LeftBottom.y;
        public static float ScreenWidth => RightTop.x - LeftBottom.x;
        public  static void  xunhuan(Transform transform)
        {
           
            
            if (transform.position.x < LeftBottom.x )
            {
                transform.position = new Vector2(RightTop.x, transform.position.y);
            }

            if (transform.position.x > RightTop.x)
            {
                transform.position =new Vector2(LeftBottom.x, transform.position.y); ;
            }

            if (transform.position.y < LeftBottom.y)
            {
                transform.position = new Vector2(transform.position.x, RightTop.y);
            }

            if (transform.position.y > RightTop.y)
            {
                transform.position = new Vector2(transform.position.x, LeftBottom.y);
            }
        
        }
    }
