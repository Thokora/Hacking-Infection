using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControllerEnemie : MonoBehaviour
{


    public GameObject shot;
    GameObject shotClone;
    public Transform shotSpawn;

    

    public float delay; //Timepo de espera para emepzar a disparar
    public float fireRate;  //Tiempo de espera volver a llamar el disparo Fire();


    private AudioSource audioSource;
    public GameObject Player; 



    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        

        InvokeRepeating("Fire", delay, fireRate); //InvokeRepeating lo que hace es generar esperas
       

    }

  

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
    }



    void Fire()
    {
       shotClone =  Instantiate(shot, shotSpawn.position, shotSpawn.rotation);//Instancia Lanza 
      //Instantiate(shot, shotSpawn.position, shotSpawn.rotation = Quaternion.identity);
        audioSource.Play();
        Destroy(shotClone, 3);
    }
}
