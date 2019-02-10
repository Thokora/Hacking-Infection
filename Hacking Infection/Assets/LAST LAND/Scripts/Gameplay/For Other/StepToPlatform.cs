using UnityEngine;
using System.Collections;

public class StepToPlatform : MonoBehaviour {


	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "IsPlatform") {
			if (other.gameObject.transform.parent.transform.parent.GetComponent<Animation> () != null) {
				other.gameObject.transform.parent.transform.parent.GetComponent<Animation> ().Stop ();
			}
			if(other.gameObject.GetComponent<AudioSource> () != null){
			other.gameObject.GetComponent<AudioSource> ().Stop ();
			}
			if (other.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Animation> () != null) {
				other.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Animation> ().Stop ();
			}
			if (other.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<AudioSource> () != null) {
				other.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<AudioSource> ().Stop ();
			}
		}
	}
}
