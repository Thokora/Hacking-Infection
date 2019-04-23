using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionEnemis : MonoBehaviour
{

    [Header("Aumento de fuerza, radio y objeto disparo") ]
    public GameObject Shooter;
    public float minForce;
    public float maxForce;
    public float radius;

    [Header("Tiempo de destruccion despues de morir")]
    public float timeDelay;

    public void Start()
    {
      //  Explode();
    }

    public void Explode()
    {
        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            Destroy(t.gameObject, timeDelay);   
        
        }
    }
}
