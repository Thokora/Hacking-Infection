using UnityEngine;
using System.Collections;

public class TriggerActiv : MonoBehaviour {

	[Header("Main Object")]
	public GameObject UseObject;
	[Header("Settings")]
	public bool ChangeGravity;
	public Vector3 ChangeForse;
	public Vector3 ChangeTorque;




	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			UseObject.SetActive (true);
				UseObject.GetComponent<Rigidbody> ().useGravity = ChangeGravity;
			UseObject.GetComponent<Rigidbody> ().velocity = ChangeForse;
				UseObject.GetComponent<Rigidbody> ().angularVelocity = ChangeTorque;
				Destroy (gameObject);
		}
	}
}
