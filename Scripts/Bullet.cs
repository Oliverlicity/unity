
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public  Vector2 direction;
    private bool pass;
 
    
    void Update()
    {
        transform.Translate(direction * Time.deltaTime*7 );
        Destroy(gameObject,2f);
    }
    

    void particlePlay(Collider2D ot)
    {
        var FX = Instantiate(Game.Default.particle);
        FX.transform.position = ot.transform.position;
        FX.gameObject.SetActive(true);
        FX.Play();
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        var rock = other.GetComponent<Rock>();
        var rigidBody = other.GetComponent<Rigidbody2D>();
        int count = FindObjectsByType<Rock>(FindObjectsInactive.Include, FindObjectsSortMode.None).Length;
        
        if (other.gameObject.name.StartsWith("Rock"))
        {
           
            Game.AddScore(5);
            //生成Ghost隐身道具
            //每10秒
            if (Game.GhostLoopTime <= 0)
            {
                Game.GhostLoopTime = 10;
                var ghost = Instantiate(Game.Default.Ghost);
                ghost.transform.position = other.transform.position;
            }
            //生成子弹散射道具
            //每10秒
            else if (Game.SpreadLoopTime <= 0)
            {
                Game.SpreadLoopTime = 10;
                var spread = Instantiate(Game.Default.Spread);
                spread.transform.position = other.transform.position;
                spread.gameObject.SetActive(true);
            }
            //生成环射时间
            //每10秒
            else if (Game.RoundLoopTime <= 0)
            {
                Game.RoundLoopTime = 10;
                var round = Instantiate(Game.Default.Round);
                round.transform.position = other.transform.position;
                round.gameObject.SetActive(true);
            }
//生成大子弹时间
//每8秒
            else if (Game.HugeLoopTime <= 0)
            {
                Game.HugeLoopTime = 8;
                var huge = Instantiate(Game.Default.Huge);
                huge.transform.position = other.transform.position;
                huge.gameObject.SetActive(true);
            }
//生成巨大化时间
//每15秒
            else if (Game.BiggerLoopTime<=0)
            {
                Game.BiggerLoopTime = 15;
                var bigger = Instantiate(Game.Default.Bigger);
                bigger.transform.position = other.transform.position;
                bigger.gameObject.SetActive(true);
            }
//生成穿梭子弹
//每11秒
            else if (Game.PassLoopTime<=0)
            {
                Game.PassLoopTime = 11f;
                var pass = Instantiate(Game.Default.Pass);
                pass.transform.position = other.transform.position;
                pass.gameObject.SetActive(true);
            }
            
            if (rock.IsBig)
            {
                // var sp =other.GetComponent<SpriteRenderer>();
                //sp.sprite = Resources.Load<Sprite>("Assets/Resources/SmallRock.png"
                Destroy(gameObject);
                rock.SmallSize();
                var rock2 = Instantiate(rock);
                rock2.transform.position = rock.transform.position+new Vector3(1.1f,1.2f,0);
                Game.Default.particle.gameObject.SetActive(true);
              
                if (count < 15)
                {
                    rock.BigSize();
                    rock.transform.position = new Vector2(Helper.LeftBottom.x, rock.transform.position.y);

                }
                else if (count >= 15 && count < 100)
                {
                    rock.BigSize();
                    rock.transform.position = new Vector2(Helper.LeftBottom.x, rock.transform.position.y);
                    rock.rb.velocity = direction * 1.5f;
                }
                else if(count>=100 && count < 200)
                {
                    rock.transform.position = new Vector2(Helper.LeftBottom.x, rock.transform.position.y);
                    rock.rb.velocity = direction * 2f;
                
                }
                else if (count >= 200 && count < 400)
                {
                    rock.transform.position = new Vector2(Helper.LeftBottom.x, rock.transform.position.y);
                    rock.rb.velocity = direction * 2.1f;
                }
            }
            else
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                particlePlay(other);
            }
            
        }
        
    }

  
}

