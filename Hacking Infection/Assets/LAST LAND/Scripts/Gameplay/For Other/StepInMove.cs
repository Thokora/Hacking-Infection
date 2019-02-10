using UnityEngine;
using System.Collections;

public class StepInMove : MonoBehaviour {

	public bool ForSteps = true;
	public bool ForPlayer = false;

	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "IsPlatform") {
			if (ForSteps){
				transform.SetParent (other.gameObject.transform);
				Destroy (GetComponent<StepInMove>());
			}
			if (ForPlayer) {
				transform.SetParent (other.gameObject.transform);
			}
		}

	}

	void OnTriggerExit(Collider other){
		if (ForPlayer) {
			transform.SetParent (GameObject.Find("Index").transform);
		}

	}

}
