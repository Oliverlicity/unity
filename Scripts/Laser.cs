using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Laser : MonoBehaviour
{
    private LineRenderer laserLine;
    public static Laser Instance;
    public Vector3 PlayerPosition;

    private Vector3 MousePosition, mouseWorld;

    //射线检测
    private RaycastHit hit;
    private Ray2D ray;

    private Vector3 Direction;

    //Rock方向
    public Vector2 direction;

    public Transform Rock;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();


    }

    private void Update()
    {

        Destroy(gameObject, 0.2f);
        MousePosition = Input.mousePosition;
        mouseWorld = Camera.main.ScreenToWorldPoint(MousePosition);
        Direction = (mouseWorld - Move.laserFirst).normalized;
        //laserLine.SetPosition(0, PlayerPosition);
        //laserLine.SetPosition(1, mouseWorld);
        LaserShoot();
    }

    public void LaserShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            bool IsHit = Physics.Raycast(Move.laserFirst, Direction,out hit );
            Debug.Log(IsHit);
            if (IsHit == true)
            {
                
            }
        }
    }

    void particlePlay(Collider2D ot)
    {
        var FX = Instantiate(Game.Default.particle);
        FX.transform.position = ot.transform.position;
        FX.gameObject.SetActive(true);
        FX.Play();
    }
    

}

