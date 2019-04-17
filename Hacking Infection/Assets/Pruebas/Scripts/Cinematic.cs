using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cinematic : MonoBehaviour
{
    public GameObject video;
    public GameObject panel;
    public float videoTime = 18f;
    VideoPlayer videoplayer;
  
    private void Awake()
    {
        videoplayer = video.GetComponent<VideoPlayer>();
        StartCoroutine(Cambio());

    }
    
IEnumerator Cambio()
{
    videoplayer.Play();
    yield return new WaitForSecondsRealtime(videoTime);
    videoplayer.Stop();
    Debug.Log("se pauso");
        video.SetActive(false);
        panel.SetActive(false);
        Debug.Log("Desaparecio");


    }

}
