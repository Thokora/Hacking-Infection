using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public float velocidad;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * velocidad * Time.deltaTime, 0, Input.GetAxis("Vertical") * velocidad * Time.deltaTime);
    }


   



}
