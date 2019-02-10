using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionInTouch : MonoBehaviour {
	[Header("Settings")]
	public float ExplosionForce;
	public float multiplier;
	[Header("Using Objects")]
	public LensFlare UseFlare;
	[Space(10)]
	public Vector3 RandomRot;
	public AudioClip[] Explosions;

	void Start () {
		gameObject.GetComponent<AudioSource> ().PlayOneShot (Explosions[Random.Range(0,Explosions.Length)]);
		StartCoroutine ("StartExplosion");
	}

	IEnumerator StartExplosion(){
		yield return null;

		var r = 2 * multiplier;
		var cols = Physics.OverlapSphere(transform.position, r);
		var rigidbodies = new List<Rigidbody>();
		foreach (var col in cols){
			if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody)){
					rigidbodies.Add (col.attachedRigidbody);
			}
		}
		foreach (var rb in rigidbodies){
			RandomRot = new Vector3 ((float)Random.Range(-1.1f, 1.1f), (float)Random.Range (-1.1f, 1.1f), (float)Random.Range (-1.1f, 1.1f));
			rb.angularVelocity = RandomRot;
			rb.AddExplosionForce(ExplosionForce * multiplier, transform.position, r,0, ForceMode.Impulse);
			if(rb.GetComponent<ForRope>()){
				rb.GetComponent<ForRope> ().StartToWork ();
			}
		}
		StartCoroutine ("FlareControl");
	}

	IEnumerator FlareControl(){
		yield return new WaitForSeconds (0.1f);
		int Point = 0;
		float SpeedBr = UseFlare.brightness / 10;
		while(Point == 0){
			if(UseFlare.brightness>0){
				UseFlare.brightness=UseFlare.brightness - SpeedBr;
			}else{
				Point = 1;
				UseFlare.gameObject.SetActive (false);
			}
			yield return new WaitForSeconds (0.01f);
		}
		yield return new WaitForSeconds (4f);
		Destroy (gameObject);
	}


}
