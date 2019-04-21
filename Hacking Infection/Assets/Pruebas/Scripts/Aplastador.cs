using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aplastador : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ForPlayer.DieFlicker = true;
        }
    }
}
