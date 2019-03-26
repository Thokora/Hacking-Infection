using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletShooter : MonoBehaviour
{


    public float speed;
    private Rigidbody rb;
    public string idEnemy = "";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //rb.transform.rotation = Quaternion.identity;

        if (idEnemy == "Enemy2")
        {
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = transform.forward * speed;
        }

    }
}
