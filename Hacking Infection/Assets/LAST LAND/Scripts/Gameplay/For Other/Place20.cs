using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Place20 : MonoBehaviour
{


	private int OldSteps;
	public string NameOfThisScene;
	public AudioClip NewMusic;

	public GameObject ShadowBef;
	public GameObject ShadowAft;

	[Header("StoryBlack")]
	public GameObject[] StepsStory;


	void Start(){
		if(PlayerPrefs.HasKey("SS" + NameOfThisScene)) {
			OldSteps = PlayerPrefs.GetInt("SS" + NameOfThisScene);
		}
	}


	public void GlobalStart(){

		int NewSteps = int.Parse(GameObject.Find("STEPS").GetComponent<Text>().text);
		if (NewSteps > OldSteps) {
			PlayerPrefs.SetInt ("AllSteps", (PlayerPrefs.GetInt ("AllSteps") + (NewSteps - OldSteps)));
			PlayerPrefs.SetInt ("SS" + NameOfThisScene, NewSteps);
		}
		int MaxLevel = PlayerPrefs.GetInt ("SavePLine");
		int CurrentLevel = (int.Parse (NameOfThisScene.Substring(1)) + 1);
		if(CurrentLevel > MaxLevel){
			PlayerPrefs.SetInt ("SavePLine", CurrentLevel);
		}

		PlayerPrefs.SetString("Elements","0");


		StartCoroutine ("GlobalProcess");
	}


	IEnumerator GlobalProcess(){
		AudioSource GetMusic = gameObject.transform.parent.GetComponent<AudioSource> ();
		AudioSource GetMusic2 = GameObject.Find("Main Camera").GetComponent<AudioSource> ();
		Image White = GameObject.Find ("White").GetComponent<Image> ();
		White.enabled = true;
		int Point = 0;
		while(Point == 0){
			if (White.color.a < 1) {
				RenderSettings.fogDensity += 0.001f;
				White.color = new Color (1, 1, 1, White.color.a + 0.001f);
				if (GetMusic.pitch > 0.1f) {
					GetMusic.pitch -= 0.002f;
					GetMusic2.pitch -= 0.002f;
				} else {
					Point = 1;
					yield return new WaitForSeconds (0.5f);
					GameObject.Find ("Finish Panel").GetComponent<Finish> ().TapToMenu ();
				}
			}
			yield return new WaitForSeconds (0.01f);
		}

	}

	// For 30 !!!

	public void GlobalStart30(){

		int NewSteps = int.Parse(GameObject.Find("STEPS").GetComponent<Text>().text);
		if (NewSteps > OldSteps) {
			PlayerPrefs.SetInt ("AllSteps", (PlayerPrefs.GetInt ("AllSteps") + (NewSteps - OldSteps)));
			PlayerPrefs.SetInt ("SS" + NameOfThisScene, NewSteps);
		}
		int MaxLevel = PlayerPrefs.GetInt ("SavePLine");
		int CurrentLevel = (int.Parse (NameOfThisScene.Substring(1)) + 1);
		if(CurrentLevel > MaxLevel){
			PlayerPrefs.SetInt ("SavePLine", CurrentLevel);
		}

		PlayerPrefs.SetString("Elements","0");


		StartCoroutine ("GlobalProcess30");
	}

	IEnumerator GlobalProcess30(){
		AudioSource GetMusic = gameObject.transform.parent.GetComponent<AudioSource> ();
		AudioSource GetMusic2 = GameObject.Find("Main Camera").GetComponent<AudioSource> ();
		Image White = GameObject.Find ("White").GetComponent<Image> ();
		White.enabled = true;
		int Point = 0;
		while(Point == 0){
			if (White.color.a < 1) {
				White.color = new Color (0, 0, 0, White.color.a + 0.02f);
				if (GetMusic2.volume > 0f) {
					GetMusic.volume -= 0.02f;
					GetMusic2.volume -= 0.02f;
				}
			}
			else {
				Point = 1;
			}
			yield return new WaitForSeconds (0.01f);
		}
		yield return new WaitForSeconds (2f);
		ShadowBef.SetActive (false);
		ShadowAft.SetActive (true);
		GetMusic.PlayOneShot (NewMusic);
		GetMusic2.gameObject.transform.position = new Vector3 (0,7,300);
		GetMusic2.gameObject.transform.eulerAngles = new Vector3 (8,180,0);
		gameObject.GetComponent<Animation> ().Play ();
		GameObject.Find ("FlagRetainer Finish").transform.localScale = new Vector3 (1,-1,1);
		while(Point == 1){
			if (White.color.a > 0) {
				White.color = new Color (0, 0, 0, White.color.a - 0.002f);
				if (GetMusic2.volume < 0.5f) {
					GetMusic.volume += 0.00125f;
					GetMusic2.volume += 0.00125f;
				}
			} else {
				Point = 2;
			}
			yield return new WaitForSeconds (0.01f);
		}
		yield return new WaitForSeconds (2f);
		int iPoint = 0;
		while(Point == 2){
			if (iPoint < StepsStory.Length) {
				StepsStory [iPoint].SetActive (true);
				StepsStory [iPoint].GetComponent<StepsSet> ().StartCoroutine ("StartCount");
				iPoint++;
			} else {
				Point = 3;
			}
			yield return new WaitForSeconds (1f);
		}
		yield return new WaitForSeconds (12f);
		while(Point == 3){
			if (White.color.a < 1) {
				White.color = new Color (0, 0, 0, White.color.a + 0.002f);
				if (GetMusic2.volume > 0f) {
					GetMusic.volume -= 0.001f;
					GetMusic2.volume -= 0.001f;
				}
			} else {
				Point = 4;
			}
			yield return new WaitForSeconds (0.01f);
		}
		GameObject.Find ("Finish Panel").GetComponent<Finish> ().TapToMenu ();
	}

}
