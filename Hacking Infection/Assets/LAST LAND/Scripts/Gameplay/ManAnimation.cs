using UnityEngine;
using System.Collections;

public class ManAnimation : MonoBehaviour {

	public Texture[] ListOfMan;
	public float TimeChange;

	void Start () {
		StartCoroutine ("StartCount");
	}
	
	public IEnumerator StartCount(){
		
		Renderer rendChange = GetComponent<Renderer>();
		int Point = 0;
		int iPoint = 0;
		while(Point < 2){
			if (Point == 0) {
				if (iPoint < ListOfMan.Length) {
					
					rendChange.material.mainTexture = ListOfMan [iPoint];
					iPoint++;
				} else {
					Point = 1;
				}
			}
			if (Point == 1) {
				if (iPoint > 0) {
					iPoint--;
					rendChange.material.mainTexture = ListOfMan [iPoint];

				} else {
					iPoint = 0;
					Point = 0;
				}
			}
			yield return new WaitForSeconds(TimeChange);
		}
	}
}
