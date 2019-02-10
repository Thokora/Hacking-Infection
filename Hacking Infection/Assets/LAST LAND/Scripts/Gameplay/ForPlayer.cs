using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForPlayer : MonoBehaviour {

	public int IsGameOver = 0;
	private GameObject IsCamera;
	private MoveCamera sIsCamera;
	private float CamSpeed;
	private float SaveHeight;
	public Image EffectsToGO;
	public GameObject Fire;

	public AudioClip ToGameOver;


	void Start(){
		IsCamera = GameObject.Find ("Main Camera");
		sIsCamera = IsCamera.GetComponent<MoveCamera> ();
		CamSpeed = sIsCamera.SpeedOfPlayer;
		SaveHeight = sIsCamera.HeightOfCam;
	}

	void OnTriggerStay(Collider other){
		if(IsGameOver == 0){
			if(other.gameObject.tag == "GOisDeep"){
				Fire.SetActive (true);
				CamSpeed *= 1.3f;
				IsGameOver = 1;
				StartCoroutine ("ProcessOfGO");
			}
			if(other.gameObject.tag == "GOisiObjects" || other.gameObject.tag == "CrystallsRed"){
				CamSpeed *= 0.7f;
				IsGameOver = 1; 
				StartCoroutine ("ProcessOfGO");
			}
		}
	}

	IEnumerator ProcessOfGO(){
		sIsCamera.enabled = false;
		gameObject.GetComponent<AudioSource> ().PlayOneShot (ToGameOver);
		EffectsToGO.enabled = true;
		int Point = 0;
		Transform gIsCamera = IsCamera.transform;
		while(Point == 0){
			if (CamSpeed > 0.05f  || CamSpeed < -0.05f) {
				if(EffectsToGO.color.a < 1){
					EffectsToGO.color = new Color (1,1,1,EffectsToGO.color.a + 0.2f);
				}
				CamSpeed = CamSpeed * 0.98f;
				gIsCamera.Translate(0,0,CamSpeed);
				gIsCamera.position = new Vector3 (gIsCamera.position.x,SaveHeight,gIsCamera.position.z);
			} else {
				Point = 1;
				GameObject.Find ("Reset Panel").GetComponent<Reset> ().StartCoroutine ("ProcessToReset");
			}
			yield return new WaitForSeconds (0.01f);
		}
	}
}
