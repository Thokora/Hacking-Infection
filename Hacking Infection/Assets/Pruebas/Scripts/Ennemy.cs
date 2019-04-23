using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ennemy : MonoBehaviour
{

    public static ExplotionEnemis explotionEnemis;

    public bool IsObstacule;

	void OnCollisionEnter(Collision input)
	{
        if (input.gameObject.tag == "Bullet")
        {
            
            explotionEnemis.Explode();
            Destroy(input.gameObject);
            ShotCentre.esperar = true;
            StartCoroutine(MuerteEnemigo());
            //m_animator.SetTrigger("Hit");
        }

    }

	void OnCollisionEnter2D(Collision2D input)
	{
		Destroy(input.gameObject);
		//m_animator.SetTrigger("Hit");
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
