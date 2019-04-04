using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletShooter : MonoBehaviour
{


    public float speed;
    private Rigidbody rb;
    public string idEnemy = "";

    public bool IsObstacule;
    //public static bool deathFlicker;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //rb.transform.rotation = Quaternion.identity;

        if (idEnemy == "Enemy2")
        {
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = transform.forward * speed;
        }

    }


    void OnCollisionEnter(Collision input)
    {
        if (input.gameObject.tag == "Player")
        {
            //deathFlicker = true;
            Destroy(input.gameObject);
            ShotCentre.esperar = true;
            StartCoroutine(MuerteEnemigo());
            //m_animator.SetTrigger("Hit");
        }

    }


    IEnumerator MuerteEnemigo()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        if (!IsObstacule)
        {
            Destroy(gameObject);
        }
    }


}
