using UnityEngine;
using System.Collections;

public class BlackMode : MonoBehaviour {

	public AudioClip SoundOfTrue;
	[Header("main mesh")]
	public Renderer[] ObjectsForMaterial;
	public Material SetMaterial;
	[Header("for different")]
	public Renderer[] DifferentObj;
	public Material[] DifferentMaterial;
	[Header("true/false")]
	public GameObject[] trueObj;
	public GameObject[] falseObj;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			gameObject.GetComponent<AudioSource> ().PlayOneShot (SoundOfTrue);


			foreach (Renderer ObjForMat in ObjectsForMaterial) {
				Material[] materials = ObjForMat.sharedMaterials;
				materials[0] = SetMaterial as Material;
				ObjForMat.GetComponent<Renderer> ().sharedMaterials = materials;
			}
				
			int Point = 0;
			int iPoint = 0;
			while (Point == 0) {
				if (iPoint < DifferentObj.Length) {
					Material[] materials = DifferentObj [iPoint].sharedMaterials;
					materials [0] = DifferentMaterial [iPoint] as Material;
					DifferentObj [iPoint].sharedMaterials = materials;
					iPoint++;
				} else {
					Point = 1;
				}
			}

			foreach (GameObject trueobj in trueObj) {
				trueobj.SetActive (true);
			}

			foreach (GameObject falseobj in falseObj) {
				falseobj.SetActive (false);
			}
		}
	}
}
