using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public GameObject flickerShield;

    [SerializeField, Range(0.0f, 10.0f)]
    public float ShieldTime;

    private void OnEnable()
    {
        StartCoroutine(EsperarShield());
    }

    IEnumerator EsperarShield()
    {
        ForPlayer.Inmunidad = true;
        yield return new WaitForSecondsRealtime(ShieldTime);
        ForPlayer.Inmunidad = false;
        flickerShield.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BulletFromEnemy")
        {
            Destroy(other.gameObject);
        }
    }


}
