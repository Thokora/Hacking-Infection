using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoosBehaviour : MonoBehaviour
{

    public GameObject Flicker; //target
    public GameObject Brazos;

    NavMeshAgent agent;

    [SerializeField, Range(0f, 30f)]
    float LookRadius;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
                FaceFlicker();
            }
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

}
