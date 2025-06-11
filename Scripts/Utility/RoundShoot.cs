using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = (-transform.position + new Vector3(1, 0, 0)).normalized;
    }

   
}
