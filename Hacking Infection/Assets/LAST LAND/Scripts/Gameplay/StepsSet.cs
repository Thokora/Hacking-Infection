using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class StepsSet : MonoBehaviour {

	public bool NoAddStep;

	public Texture[] ListOfStep;


	void Start(){
		if (!NoAddStep) {
			gameObject.GetComponent<AudioSource> ().pitch = Random.Range (0.8F, 1.2F);
		}
	}


	public IEnumerator StartCount(){
		yield return new WaitForSeconds(0.017f);
	 	Renderer rendChange = GetComponent<Renderer>();
		int Point = 0;
		int iPoint = 0;
		while(Point == 0){
			if (iPoint < ListOfStep.Length) {
				rendChange.material.mainTexture = ListOfStep [iPoint];
				iPoint++;
			} else {
				Point = 1;
			}
			yield return new WaitForSeconds(0.017f);
		}
	}

	void OnBecameInvisible(){
		Destroy(gameObject);
	}


}
