using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ennemy : MonoBehaviour
{


    [Header("Aumento de fuerza, radio y objeto disparo")]
    public GameObject Shooter;
    public float minForce;
    public float maxForce;
    public float radius;

    [Header("Tiempo de destruccion despues de morir")]
    public float timeDelay;


    public bool IsObstacule;
    public Rigidbody rb;




    void OnCollisionEnter(Collision input)
    {
        if (input.gameObject.tag == "Bullet")
        {
            ForPlayer.BulletImpactEnemy = true;
            rb.useGravity = true;
            ShotCentre.esperar = true;
            Destroy(input.gameObject);
            StartCoroutine(MuerteEnemigo());
            //m_animator.SetTrigger("Hit");
        }

    }

    void OnCollisionEnter(Collision2D input)
    {
        Destroy(input.gameObject);
        //m_animator.SetTrigger("Hit");
    }



    IEnumerator MuerteEnemigo()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (!IsObstacule)
        {
            Explode(); //se hace el llamado del Scripti que hace explotar los enemigos
            //Destroy(gameObject);
        }
    }



//Script para hacer explotar a los enemigos 
    public void Explode()
    {
        foreach (Transform t in transform)
        {


            rb = t.GetComponent<Rigidbody>();
            rb.useGravity = false;

            if (rb != null)
            {

                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            Destroy(t.gameObject, timeDelay);
            Destroy(gameObject, timeDelay);

        }
    }



}
