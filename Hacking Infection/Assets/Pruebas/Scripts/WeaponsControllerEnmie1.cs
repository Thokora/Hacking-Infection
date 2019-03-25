using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsControllerEnmie1 : MonoBehaviour
{

    public GameObject shotLaser;
    public Transform shotSpawn;



    public float delay; //Timepo de espera para emepzar a disparar
    public float fireRate;  //Tiempo de espera volver a llamar el disparo Fire();


    private AudioSource audioSource;




    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {

        InvokeRepeating("Fire", delay, fireRate); //InvokeRepeating lo que hace es generar esperas


    }



    // Update is called once per frame
    void Update()
    {

    }



    void Fire()
    {
        Instantiate(shotLaser, shotSpawn.position, shotSpawn.rotation);//Instancia rashoLaser
        //Instantiate(shot, shotSpawn.position, shotSpawn.rotation = Quaternion.identity);
        audioSource.Play();
    }
}
