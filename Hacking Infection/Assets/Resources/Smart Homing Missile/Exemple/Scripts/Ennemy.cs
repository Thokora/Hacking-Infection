using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ennemy : MonoBehaviour
{
	/*Animator m_animator;


	void Start()
	{
		m_animator = GetComponent<Animator>();
	}
    */
	void OnCollisionEnter(Collision input)
	{
		Destroy(input.gameObject);
        ShotCentre.esperar = true;
        StartCoroutine(MuerteEnemigo());
		//m_animator.SetTrigger("Hit");
	}

	void OnCollisionEnter2D(Collision2D input)
	{
		Destroy(input.gameObject);
		//m_animator.SetTrigger("Hit");
	}

    IEnumerator MuerteEnemigo()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(gameObject);
    }

}
