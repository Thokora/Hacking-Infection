using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cinematic : MonoBehaviour
{


    public VideoPlayer videoplayer;


    int num = 1;

    float tiempo = 0;
    float tiempoMax = 6;



    private void Awake()
    {
        videoplayer = GetComponent<VideoPlayer>();
        Video();
    }

    void Start()
    {

        
    }

    void Update()
    {
        
    }

    public void Video()
    {

        tiempo += Time.deltaTime;
        Debug.Log(tiempo.ToString());

        if (tiempo < tiempoMax)
        {
            videoplayer.Play();
            num++;
        }
        if (num == tiempoMax)
        {
            videoplayer.Stop();
            Debug.Log("Me pause");

           
        }

        

    }

   
}
