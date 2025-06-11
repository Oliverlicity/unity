using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huge : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity=(-transform.position+new Vector3(1.5f,0,0)).normalized*0.5f;
    }

   
}
