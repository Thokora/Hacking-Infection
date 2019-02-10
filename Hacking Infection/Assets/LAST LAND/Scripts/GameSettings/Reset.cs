using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {

	private Image TableOfReset;
	private Text PAUSE;
	public string NameOfSMenu;
	private AudioSource ThisIndex;

	[Header("Other Objects")]
	public Image LoadingIm;
	public Text LoadingTe;

	void Start () {
		ThisIndex = GameObject.Find ("Index").GetComponent<AudioSource> ();
		TableOfReset = GameObject.Find ("Reset Panel").GetComponent<Image>();
		PAUSE = GameObject.Find ("PAUSE").GetComponent<Text>();
	}

	public IEnumerator ProcessToReset(){
		int Point = 0;
		TableOfReset.enabled = true;
		TableOfReset.transform.localPosition = new Vector3 (-10,0,0);
		while(Point == 0){
			if(PAUSE.color.a > 0){
				PAUSE.color = new Color(PAUSE.color.r,PAUSE.color.g,PAUSE.color.b,PAUSE.color.a - 0.05f);
				if (ThisIndex.pitch >= 0.05f) {
					ThisIndex.pitch -= 0.05f;
				} else {
					ThisIndex.pitch = 0;
				}
				TableOfReset.color = new Color(TableOfReset.color.r,TableOfReset.color.g,TableOfReset.color.b,TableOfReset.color.a + 0.05f);
				if(TableOfReset.transform.localPosition.x < 0){
					TableOfReset.transform.localPosition = new Vector3 (TableOfReset.transform.localPosition.x + 0.5f,0,0);
				}
			}else{
				Point = 1;
				GameObject.Find("Yes Reset").GetComponent<Image> ().enabled = true;
				GameObject.Find("No Reset").GetComponent<Image> ().enabled = true;
				PAUSE.enabled = false;
				GameObject.Find("Main Camera").GetComponent<MoveCamera> ().enabled = false;
				GameObject.Find("Main Camera").GetComponent<ShotCentre> ().enabled = false;
			}
			yield return new WaitForSeconds(0.01f);
		}
		if(PlayerPrefs.HasKey (GameObject.Find ("Finish Panel").GetComponent<Finish> ().NameOfThisScene + "i")){
		int was_in = PlayerPrefs.GetInt (GameObject.Find ("Finish Panel").GetComponent<Finish> ().NameOfThisScene + "i");
		was_in++;
		PlayerPrefs.SetInt (GameObject.Find ("Finish Panel").GetComponent<Finish> ().NameOfThisScene + "i", was_in);
		}
	}

	public void TapToReset(){
		Time.timeScale = 1;
		StartCoroutine ("ProcessToReload");
	}

	IEnumerator ProcessToReload(){
		GameObject.Find("Yes Reset").GetComponent<Image> ().enabled = false;
		GameObject.Find("No Reset").GetComponent<Image> ().enabled = false;
		int Point = 0;
		while(Point == 0){
			if(TableOfReset.color.a > 0){
				TableOfReset.color = new Color(TableOfReset.color.r,TableOfReset.color.g,TableOfReset.color.b,TableOfReset.color.a - 0.1f);
				if(TableOfReset.color.a <= 0.5){
					TableOfReset.transform.localPosition = new Vector3 (TableOfReset.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;
				TableOfReset.enabled = false;
			}
			yield return new WaitForSeconds(0.01f);
		}
		AudioListener.volume = 0;
		GameObject.Find ("Black Tr").GetComponent<Animation> ().Play ("BlackTrAClose");
		LoadingIm.gameObject.SetActive (true);
		LoadingTe.gameObject.SetActive (true);
		LoadingIm.enabled = true;
		LoadingTe.enabled = true;
		while (Point == 1) {
			if (LoadingIm.color.a < 1) {
				LoadingIm.color = new Color (1, 1, 1, LoadingIm.color.a + 0.05f);
				LoadingTe.color = new Color (1, 1, 1, LoadingTe.color.a + 0.05f);
			} else {
				Point = 2;
			}
			yield return new WaitForSeconds (0.01f);
		}
		SceneManager.LoadScene ("Place " + GameObject.Find("Finish Panel").GetComponent<Finish>().NameOfThisScene);
	}

	public void TapToMenu(){
		Time.timeScale = 1;
		StartCoroutine ("ProcessToMenu");
	}

	IEnumerator ProcessToMenu(){
		GameObject.Find("Yes Reset").GetComponent<Image> ().enabled = false;
		GameObject.Find("No Reset").GetComponent<Image> ().enabled = false;
		int Point = 0;
		while(Point == 0){
			if(TableOfReset.color.a > 0){
				TableOfReset.color = new Color(TableOfReset.color.r,TableOfReset.color.g,TableOfReset.color.b,TableOfReset.color.a - 0.1f);
				if(TableOfReset.color.a <= 0.5){
					TableOfReset.transform.localPosition = new Vector3 (TableOfReset.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;
				TableOfReset.enabled = false;
			}
			yield return new WaitForSeconds(0.01f);
		}
		AudioListener.volume = 0;
		GameObject.Find ("Black Tr").GetComponent<Animation> ().Play ("BlackTrAClose");
		LoadingIm.gameObject.SetActive (true);
		LoadingTe.gameObject.SetActive (true);
		LoadingIm.enabled = true;
		LoadingTe.enabled = true;
		while (Point == 1) {
			if (LoadingIm.color.a < 1) {
				LoadingIm.color = new Color (1, 1, 1, LoadingIm.color.a + 0.05f);
				LoadingTe.color = new Color (1, 1, 1, LoadingTe.color.a + 0.05f);
			} else {
				Point = 2;
			}
			yield return new WaitForSeconds (0.01f);
		}
		SceneManager.LoadScene (NameOfSMenu);
	}

	public void StartToLearn(){
		StartCoroutine ("ProcessToLearn");
	}

	IEnumerator ProcessToLearn(){
		int Point = 1;
		AudioListener.volume = 0;
		GameObject.Find ("Black Tr").GetComponent<Animation> ().Play ("BlackTrAClose");
		LoadingIm.gameObject.SetActive (true);
		LoadingTe.gameObject.SetActive (true);
		LoadingIm.enabled = true;
		LoadingTe.enabled = true;
		while (Point == 1) {
			if (LoadingIm.color.a < 1) {
				LoadingIm.color = new Color (1, 1, 1, LoadingIm.color.a + 0.05f);
				LoadingTe.color = new Color (1, 1, 1, LoadingTe.color.a + 0.05f);
			} else {
				Point = 2;
			}
			yield return new WaitForSeconds (0.01f);
		}
		SceneManager.LoadSceneAsync ("Learn");
	}
}
