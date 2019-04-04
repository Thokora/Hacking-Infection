using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionEnemis : MonoBehaviour
{

    [Header("Agregar fuerza y destrucción de las partes")]
    public float timeDelay;
    public float minForce;
    public float maxForce;
    public float radius;
    public GameObject disparo;

    private float timeEspera;


    private void Start()
    {
        Explode();
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
            Destroy(disparo, timeDelay);

        }
    }
}
