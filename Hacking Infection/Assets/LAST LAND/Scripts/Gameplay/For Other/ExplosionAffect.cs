using UnityEngine;
using System.Collections;

public class ExplosionAffect : MonoBehaviour
{

	void OnTriggerEnter(Collider other){
		other.gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range(-2,2),Random.Range(0,30),Random.Range(-30,0));
	}
}

