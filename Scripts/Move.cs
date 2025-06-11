using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private Transform tf;
    private Rigidbody2D rb;
    public Transform bullet;
    private AudioSource sound;
    private SpriteRenderer sprite;
    public Transform camera;
    public Transform laser;
    //激光位置
    public static Vector3 laserFirst;
    public static Vector3 laserSecond;
    void Start()
    {
      tf=  GetComponent<Transform>();
      rb = GetComponent<Rigidbody2D>();
      sound = GetComponent<AudioSource>();
      sprite = GetComponent<SpriteRenderer>();
        bigger(2f);
    }

    void bigger(float bigger)
    {
        var bullet = Resources.Load("Bullet");
        var Bullet =bullet.GetComponent<Transform>();
        Bullet.transform.localScale = new Vector2(bigger,bigger);
    }
    void Update()
    {
        //KeyMove();
        MouseMove(); 
        Shoot(transform.up);
        
        Helper.xunhuan(this.transform);

        #region 生效时间
        //Ghost生效时间过后
        if (IsGhost==true)
        {
            GhostTime-=Time.deltaTime;
            if (GhostTime<=0)
            {
                GhostTime = 0;
                IsGhost = false;
                sprite.color =new Vector4(1,1,1,1f);
                Speed = 3;
                //var UIPower=Game.Default.transform.Find("UI/PowerUps/ghost");
                //UIPower.GetComponent<Image>();
                //UIPower.gameObject.SetActive(false);
                var UIPower = UIPowerUps.UIPowerups.ghost;
                UIPower.gameObject.SetActive(false);

            }
        }
        //散射时间过后
        if (IsSpread==true)
        {
            SpreadTime-=Time.deltaTime;
            SpreadShoot();
            if (SpreadTime<=0)
            {
                SpreadTime = 0;
                IsSpread=false;
                
                // var UIPower=Game.Default.transform.Find("UI/PowerUps/spread");
                // UIPower.GetComponent<Image>();
                // UIPower.gameObject.SetActive(false);
                var UIPower = UIPowerUps.UIPowerups.spread;
                UIPower.gameObject.SetActive(false);
            }
        }

        if (IsRound==true)
        {
            RoundTime-=Time.deltaTime;
            RoundShoot();
            if (RoundTime<=0)
            {
                RoundTime = 0;
                IsRound=false;
                
                var UIPower = UIPowerUps.UIPowerups.round;
                UIPower.gameObject.SetActive(false);
               
            }
        }

        if (IsBigger==true)
        {
            BiggerTime-=Time.deltaTime;
            if (BiggerTime<=0)
            {
                BiggerTime = 0;
                IsBigger = false;
                
                var UIPower = UIPowerUps.UIPowerups.bigger;
                UIPower.gameObject.SetActive(false);
                //变回原样
                bigger(2f);
            }
           
        }
        if (IsHuge==true)
        {
            HugeTime-=Time.deltaTime;
            if (HugeTime<=0)
            {
                HugeTime = 0;
                IsHuge = false;
                    
                var UIPower = UIPowerUps.UIPowerups.huge;
                UIPower.gameObject.SetActive(false);
                    
                tf.localScale = new Vector2(0.5f,0.5f);
                IsFirst = false;
            }
        }

        if (IsPass==true)
        {
           LaserShoot();
            PassTime-=Time.deltaTime;
            if (PassTime<=0)
            {
               
                PassTime = 0;
                IsPass = false;
               
                var UIPower = UIPowerUps.UIPowerups.pass;
                UIPower.gameObject.SetActive(false);
            }
        }
        #endregion
    }

    public float Speed=3;
    //转向跟随鼠标
       
    void MouseMove()
    {

        if (Input.GetMouseButton(1))
        {
            var mousePositon =Input.mousePosition;
            var mouseWorld = Camera.main.ScreenToWorldPoint(mousePositon);
            var direction = (Vector2)((mouseWorld - transform.position).normalized);
            transform.up = direction;
            //transform.Translate(transform.up * Speed * Time.deltaTime);
            var targetVelocity = transform.up * Speed;
            //惯性公式
            rb.velocity = Vector2.Lerp(rb.velocity,targetVelocity,1-Mathf.Exp(-Time.deltaTime*1f));
        }
       
    }

    void KeyMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector2.up*Speed;
           
        }

        if (Input.GetKey(KeyCode.A))
        {
            tf.Rotate(new Vector3(0, 0,-0.1f));
            rb.velocity = Vector2.left*Speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            tf.Rotate(new Vector3(0,0,0.1f));
            rb.velocity = Vector2.right*Speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Vector2.down*Speed;
           
        }
    }
        

    void SpreadShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
         // var bulletTemplate=  transform.Find("Bullet");
        var bulletShoot =Instantiate(bullet);

        bulletShoot.transform.position = transform.position;
        //bulletShoot.transform.Translate(new Vector3(0,1,0)*Time.deltaTime);
        
        var bulletComponent=bulletShoot.GetComponent<Bullet>();
        bulletComponent.direction = transform.up;
        sound.Play();
       
        //发射散射
            if (IsSpread=true)
             {
                float angle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
             var angleUp= angle + 12 ;
             var angleDown = angle - 12;
             Shoot(new Vector2(Mathf.Cos(angleUp*Mathf.Deg2Rad), Mathf.Sin(angleUp*Mathf.Deg2Rad)));
             Shoot(new Vector2(Mathf.Cos(angleDown*Mathf.Deg2Rad), Mathf.Sin(angleDown*Mathf.Deg2Rad)));
            }
        }
    }
    //道具散射方法
    void Shoot(Vector2 direction)
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bulletShoot =Instantiate(bullet);

            bulletShoot.transform.position = transform.position;
            //bulletShoot.transform.Translate(new Vector3(0,1,0)*Time.deltaTime);
        
            var bulletComponent=bulletShoot.GetComponent<Bullet>();
            bulletComponent.direction = direction;
            //bulletComponent.direction = transform.up;
            sound.Play();
        }
       
    }
    public void LaserShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePositon =Input.mousePosition;
            var mouseWorld = Camera.main.ScreenToWorldPoint(mousePositon);
            
            var laserShoot =Instantiate(laser);
            var laserPosition =laserShoot.GetComponent<LineRenderer>();
            laserPosition.SetPosition(0,transform.position);
            laserFirst =laserPosition.GetPosition(0);
            laserPosition.SetPosition(1,mouseWorld);
            laserSecond =laserPosition.GetPosition(1);
        }
    }
    void RoundShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // var bulletTemplate=  transform.Find("Bullet");
            var bulletShoot = Instantiate(bullet);

            bulletShoot.transform.position = transform.position + transform.up;
            //bulletShoot.transform.Translate(new Vector3(0,1,0)*Time.deltaTime);

            var bulletComponent = bulletShoot.GetComponent<Bullet>();
            bulletComponent.direction = transform.up;
            sound.Play();

            //发射散射
            if (IsSpread = true)
            {
                float angle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;

                for (float angleUp = 0; angleUp <= 360; angleUp += 20)
                {
                    Shoot(new Vector2(Mathf.Cos(angleUp * Mathf.Deg2Rad), Mathf.Sin(angleUp * Mathf.Deg2Rad)));

                }

            }
        }
    }

    #region TimeandIs
    //Ghost
    public bool IsGhost=false;
    public float GhostTime = 0;
    //Spread 
    public bool IsSpread = false;
    public float SpreadTime=0;
    //RoundShoot
    public bool IsRound=false;
    public  float RoundTime=0;
    //Bigger
    public bool IsBigger = false ;
    public float BiggerTime = 0;
    //Huge
    public bool IsHuge = false;
    public float HugeTime = 0;
    private bool IsFirst = false;
    //Pass
    public bool IsPass = false;
    public float PassTime = 0;
    #endregion TimeandIs
    void OnTriggerEnter2D(Collider2D other)
    {
       // Destroy(gameObject);

       #region Rock
       if (other.gameObject.name.StartsWith("Rock"))
       {
           if(IsGhost==true)
               return;
           if (IsHuge == true)
           {
               if(IsGhost==true)
                   return;
               tf.localScale = new Vector2(1f,1f);
               transform.position =transform.position ;
               IsFirst=true;
               tf.DOShakePosition(1f, 0.5f, 8);
               if (IsFirst==true)
               {
                   tf.localScale = new Vector2(0.5f,0.5f);
                   var UIPower = UIPowerUps.UIPowerups.huge;
                   UIPower.gameObject.SetActive(false);
                   IsFirst=false;
                   
               }
           }
           else 
           {
               //Debug.Log("collision");
               AudioSource Death= transform.Find("Lose").GetComponent<AudioSource>();
               Death.Play();
               this.gameObject.SetActive(false);
               Game.ReLoadScene();
               camera.DOShakePosition(1, 0.5f, 8);
           }
           
       }
       

       #endregion
       
         else if(other.gameObject.name.StartsWith("Ghost"))
        {
            IsGhost = true;
            Speed = 5;
            GhostTime = 5;
            var ghost=GetComponent<Ghost>();
            sprite.color =new Vector4(1,1,1,0.4f);
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            var FX =Instantiate(Game.Default.particle);
            FX.transform.position = other.transform.position;
            FX.gameObject.SetActive(true);
            FX.Play();
           Game.Default.audio.Play();
           

           var UIPower=Game.Default.transform.Find("UI/PowerUps/ghost");
           UIPower.GetComponent<Image>();
           UIPower.gameObject.SetActive(true);
           
           
        }

        else if (other.gameObject.name.StartsWith("Spread"))
        {
            IsSpread = true;
            SpreadTime = 5;
            other.gameObject.SetActive(false);
            Game.Default.audio.Play();
            //粒子特效
            var FX =Instantiate(Game.Default.particle);
            FX.transform.position = other.transform.position;
            FX.gameObject.SetActive(true);
            FX.Play();
            
            var UIPower=Game.Default.transform.Find("UI/PowerUps/spread");
            UIPower.GetComponent<Image>();
            UIPower.gameObject.SetActive(true);
            
        }
        else if (other.gameObject.name.StartsWith("RoundShoot"))
        {
            RoundTime = 3f;
            IsRound = true;
            other.gameObject.SetActive(false);
            Game.Default.audio.Play();
            
            var UIPower=Game.Default.transform.Find("UI/PowerUps/round");
            UIPower.GetComponent<Image>();
            UIPower.gameObject.SetActive(true);
        }
        else if (other.gameObject.name.StartsWith("Bigger"))
        {
            BiggerTime = 10;
            IsBigger=true;
            other.gameObject.SetActive(false);
            Game.Default.audio.Play();
            
            var UIPower=Game.Default.transform.Find("UI/PowerUps/bigger");
            UIPower.GetComponent<Image>();
            UIPower.gameObject.SetActive(true);
            if (BiggerTime>0 && IsBigger==true)
            {
             bigger(4f);
            }
        }
        else if (other.gameObject.name.StartsWith("Huge"))
        {
            HugeTime = 5f;
            IsHuge=true;
            IsFirst=false;
            other.gameObject.SetActive(false);
            Game.Default.audio.Play();
            //
            tf.localScale = new Vector2(1.5f,1.5f);
            
            var UIPower=Game.Default.transform.Find("UI/PowerUps/huge");
            UIPower.GetComponent<Image>();
            UIPower.gameObject.SetActive(true);
            
        }
        else if (other.gameObject.name.StartsWith("Pass"))
       {
           PassTime = 12f;
           IsPass = true;
         
           other.gameObject.SetActive(false);
           Game.Default.audio.Play();
            
           var UIPower=Game.Default.transform.Find("UI/PowerUps/pass");
           UIPower.GetComponent<Image>();
           UIPower.gameObject.SetActive(true);
          
       }
    }

  
}
