﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletShooter : MonoBehaviour
{


    public float speed;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

     //rb.transform.rotation = Quaternion.identity;

        rb.velocity = transform.forward * speed;
 
    }
}
