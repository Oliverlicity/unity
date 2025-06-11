using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = (-transform.position+new Vector3(1.8f,1.2f,0)).normalized; ;
    }

    
}
