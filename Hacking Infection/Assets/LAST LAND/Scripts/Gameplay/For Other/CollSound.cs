using UnityEngine;
using System.Collections;

public class CollSound : MonoBehaviour
{

	public Vector2 MinMaxVelosity;

	private Rigidbody SaveComponent;
	private AudioSource SaveComponent2;

	private bool Waiter = true;

	void Start(){
		SaveComponent = gameObject.GetComponent<Rigidbody> ();
		SaveComponent2 = gameObject.GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision other){
		if (Waiter) {
			float AllVelosity = Mathf.Abs (SaveComponent.velocity.x) + Mathf.Abs (SaveComponent.velocity.y) + Mathf.Abs (SaveComponent.velocity.z);
			if (AllVelosity > MinMaxVelosity.x) {
				if (AllVelosity < MinMaxVelosity.y) {
					SaveComponent2.volume = AllVelosity / MinMaxVelosity.y;
					SaveComponent2.pitch = 1 + SaveComponent2.volume;
				} else {
					SaveComponent2.volume = 1;
					SaveComponent2.pitch = 1 + SaveComponent2.volume;
				}
				SaveComponent2.Play ();
				Waiter = false;
				StartCoroutine ("ItTime");
			}
		}
	}

	IEnumerator ItTime(){
		yield return new WaitForSeconds (0.1f);
		Waiter = true;
	}

}

