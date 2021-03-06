﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControllerEnmie1 : MonoBehaviour
{

    public GameObject shotLaser; //Bala
    private GameObject cloneLaser;
    public Transform shotSpawn; // PadreBala
    public GameObject Player;




    public float delay; //Timepo de espera para emepzar a disparar
    public float fireRate;  //Tiempo de espera volver a llamar el disparo Fire();


    private AudioSource audioSource;
    private Rigidbody rb;




    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {

        InvokeRepeating("Fire", delay, fireRate); //InvokeRepeating lo que hace es generar esperas


    }



    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform) ;
    }



    void Fire()
    {
       cloneLaser =  Instantiate(shotLaser, shotSpawn.position, shotSpawn.rotation);//Instancia rashoLaser
        //Instantiate(shot, shotSpawn.position, shotSpawn.rotation = Quaternion.identity);
        audioSource.Play();
        Destroy(cloneLaser, 3);
    }
}
