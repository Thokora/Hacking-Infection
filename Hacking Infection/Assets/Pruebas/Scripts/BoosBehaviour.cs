using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoosBehaviour : MonoBehaviour
{

    [SerializeField]
    GameObject Flicker; //target

    [SerializeField]
    GameObject Brazos;

    NavMeshAgent agent;

    [SerializeField, Range(0f, 30f)]
    float LookRadius;

    [SerializeField]
    GameObject m_projectileIzq;
    [SerializeField]
    GameObject m_projectileDer;

    [SerializeField, Range(0, 20)]
    float m_launchIntensity;

    public GameObject[] BulletPosIzq;
    public GameObject[] BulletPosDer;

    int idBulletIzq;
    int idBulletDer;
    bool disparar;

    Animator BossAnim;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        BossAnim = GetComponent<Animator>();

        StartCoroutine(FireDerWait());
        StartCoroutine(FireIzqWait());
    }

    private void Update()
    {
        FollowFlicker();
        ArmFlicker();

    }

    void FollowFlicker()
    {
        float distance = Vector3.Distance(Flicker.transform.position, transform.position); //escribe el valor que hay entre el target(flicker) y el objeto que tiene el script (boss)
        
        if (distance <= LookRadius)
        {
            agent.SetDestination(Flicker.transform.position);
            Debug.Log("Esta dentro del rango");
            
            if (distance <= agent.stoppingDistance)
            {
                BossAnim.SetBool("Andar",false);
                FaceFlicker();
            }else
            {
                BossAnim.SetBool("Andar", true);
            }
        }else
        {
            BossAnim.SetBool("Andar", false);
        }

        if (distance <= (LookRadius +5))
        {
            disparar = true;
        }
        else
        {
            disparar = false;
        }

    }

    void FaceFlicker()
    {
        Vector3 direction = (Flicker.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * 5f);
    }

    void ArmFlicker()
    {
        Vector3 LookToFlicker = new Vector3(Brazos.transform.position.x, Flicker.transform.position.y, Flicker.transform.position.z);

        Brazos.transform.LookAt(LookToFlicker);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }


    public void FireDer() //Funcion agregada por Fawer
    {
        GameObject newProjectile = Instantiate(m_projectileDer, BulletPosDer[idBulletDer].transform.position, BulletPosDer[idBulletDer].transform.rotation) as GameObject;

        if (newProjectile.GetComponent<Rigidbody2D>())
            newProjectile.GetComponent<Rigidbody2D>().AddForce(BulletPosDer[idBulletDer].transform.forward * m_launchIntensity, ForceMode2D.Impulse);
        else if (newProjectile.GetComponent<Rigidbody>())
            newProjectile.GetComponent<Rigidbody>().AddForce(BulletPosDer[idBulletDer].transform.forward * m_launchIntensity, ForceMode.Impulse);

        idBulletDer++;
    }

    public void FireIzq() //Funcion agregada por Fawer
    {
        GameObject newProjectile = Instantiate(m_projectileIzq, BulletPosIzq[idBulletIzq].transform.position, BulletPosIzq[idBulletIzq].transform.rotation) as GameObject;

        if (newProjectile.GetComponent<Rigidbody2D>())
            newProjectile.GetComponent<Rigidbody2D>().AddForce(BulletPosIzq[idBulletIzq].transform.forward * m_launchIntensity, ForceMode2D.Impulse);
        else if (newProjectile.GetComponent<Rigidbody>())
            newProjectile.GetComponent<Rigidbody>().AddForce(BulletPosIzq[idBulletIzq].transform.forward * m_launchIntensity, ForceMode.Impulse);

        idBulletIzq++;
    }

    IEnumerator FireDerWait()
    {
        if (disparar)
        {
            yield return new WaitForSecondsRealtime(1f);
            FireDer();
            if (idBulletDer == BulletPosDer.Length)
            {
                idBulletDer = 0;
            }
            StartCoroutine(FireDerWait());
        }
        else
        {
            yield return new WaitForSecondsRealtime(2f);
            StartCoroutine(FireDerWait());
        }

    }


    IEnumerator FireIzqWait()
    {
        if (disparar)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            FireIzq();
            if (idBulletIzq == BulletPosIzq.Length)
            {
                idBulletIzq = 0;
            }
            StartCoroutine(FireIzqWait());
        }
        else
        {
            yield return new WaitForSecondsRealtime(2f);
            StartCoroutine(FireIzqWait());
        }
    }



}
