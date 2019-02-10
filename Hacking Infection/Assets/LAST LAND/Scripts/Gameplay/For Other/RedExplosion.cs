using UnityEngine;
using System.Collections;

public class RedExplosion : MonoBehaviour
{

	public Transform RedMesh;
	public AudioClip ExplosionSound;

	private int Once = 0;


	void OnCollisionEnter(Collision other){
		if (Once == 0) {
			Once = 1;
			StartCoroutine ("ProcessToExplosion");
		}
	}


	IEnumerator ProcessToExplosion(){
		gameObject.GetComponent<AudioSource> ().PlayOneShot (ExplosionSound);
		RedMesh.gameObject.SetActive (true);
		int Point = 0;
		while(Point == 0){
			if (RedMesh.position.z > -500) {
				RedMesh.position = new Vector3 (0, 0, RedMesh.position.z - 5);
			} else {
				Point = 1;
				RedMesh.gameObject.SetActive (false);
			}
			yield return new WaitForSeconds (0.01f);
		}
	}

}

