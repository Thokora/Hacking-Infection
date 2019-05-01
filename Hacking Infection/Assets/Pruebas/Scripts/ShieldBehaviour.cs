using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    public GameObject ShieldMaster;
    public bool BelongsToFlicker;
    public bool isPlatform = false;
    Animator animShield;

    [SerializeField, Range(0.0f, 10.0f)]
    public float ShieldTime;

    private void OnEnable()
    {
        if (isPlatform == true)
        {
            animShield = ShieldMaster.GetComponent<Animator>();
            animShield.SetBool("Platform", true);
        }
        StartCoroutine(EsperarShield());
    }

    IEnumerator EsperarShield()
    {
        if (BelongsToFlicker)
        {
            ForPlayer.Inmunidad = true;
            yield return new WaitForSecondsRealtime(ShieldTime);
            ForPlayer.Inmunidad = false;
            ShieldMaster.SetActive(false);
        }else //escudo del enemigo
        {
            yield return new WaitForSecondsRealtime(ShieldTime);
            ShieldMaster.SetActive(false);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BelongsToFlicker)
        {
            if (other.gameObject.tag == "BulletFromEnemy")
            {
                Destroy(other.gameObject);
            }
        }else //escudo del enemigo
        {
            if (other.gameObject.tag == "Bullet")
            {
                Destroy(other.gameObject);
            }
        }

    }


}
