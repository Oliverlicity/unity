using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Default;
    private Text mTextScore;
    public static int Score = 0;
//隐身Ghost生成时间
    public static float GhostLoopTime = 0;
    public Ghost Ghost;
    //粒子特效
    public ParticleSystem particle;
    public AudioSource audio;
    //Spread生效时间
    public static float SpreadLoopTime = 0;
    public Spread Spread;
    //Round生效时间
    public static float RoundLoopTime = 0;
    public RoundShoot Round;
    //huge循环时间
    public static float HugeLoopTime = 0;
    public Huge Huge; 
    //Bigger循环时间
    public static float BiggerLoopTime = 0;
    public Bigger Bigger; 
    //Pass循环时间
    public static float PassLoopTime = 0;
    public Pass Pass;
    //UI
    public UIPowerUps UIPowerUps;
    public UIPowerUp UIPowerUp;
    
    private void Awake()
    {
        Default = this;
        transform.Find("Ghost").GetComponent<Ghost>();
        transform.Find("UI/PowerUpFX/Lighting").GetComponent<ParticleSystem>();
        transform.Find("UI/PowerUps").GetComponent<UIPowerUps>();
        transform.Find("UI/PowerUps/ghost").GetComponent<UIPowerUp>();
       
    }

    private void OnDestroy()
    {
        Default = null;
    }

    void Start()
    {
        //Ghost循环时间
        GhostLoopTime = 10;
        //Spread循环时间
        SpreadLoopTime = 10;
        //Round循环时间
        RoundLoopTime = 15;
        //Huge循环时间
        HugeLoopTime = 20;
        //bigger循环时间
        BiggerLoopTime = 10;
        //pass循环时间
        PassLoopTime = 8;
        //计数文本获取
        mTextScore = transform.Find("UI/Score").GetComponent<Text>();
        Score = 0;
        //粒子声音获取
        audio = transform.Find("UI/PowerUpFX/Lighting").GetComponent<AudioSource>();
    }
        //分数计算
    public static void AddScore(int score)
    {
        Score += score;
        Default.mTextScore.text = "Score: "+Score.ToString();
    }
    //场景加载
    public static IEnumerator SceneReLoad()
    {
        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene("Game");
    }

    public static void ReLoadScene()
    {
        Default.StartCoroutine(SceneReLoad());
    }

    private void Update()
    {
        GhostLoopTime -= Time.deltaTime;
        SpreadLoopTime -= Time.deltaTime;
        RoundLoopTime -= Time.deltaTime;
        HugeLoopTime -= Time.deltaTime;
        BiggerLoopTime-= Time.deltaTime;
        PassLoopTime -= Time.deltaTime;
    }
}
