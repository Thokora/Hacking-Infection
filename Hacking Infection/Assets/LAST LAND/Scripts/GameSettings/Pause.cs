using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	
	private Image TableOfPause;
	private Text PAUSE;
	public string NameOfSMenu;

	private AudioSource ThisIndex;

	[Header("Other Objects")]
	public Image LoadingIm;
	public Text LoadingTe;

    public GameObject panelVideo;

    private void Awake()
    {
        AudioListener.volume = 1;
        ThisIndex = GameObject.Find("Index").GetComponent<AudioSource>();
        TableOfPause = GameObject.Find("Pause Panel").GetComponent<Image>();
        PAUSE = GameObject.Find("PAUSE").GetComponent<Text>();
        TapToPause();
        StartCoroutine(IniciarVideo());
    }

    IEnumerator IniciarVideo()
    {
        panelVideo.SetActive(true);
        yield return new WaitForSecondsRealtime(18f);
        TapToContunie();
    }

	public void TapToPause(){
		if(PAUSE.color.a == 1){
		StartCoroutine ("ProcessToPause");
		}
	}

	IEnumerator ProcessToPause(){
		int Point = 0;
		TableOfPause.enabled = true;
		TableOfPause.transform.localPosition = new Vector3 (-10,0,0);
		while(Point == 0){
			if(PAUSE.color.a > 0){
				PAUSE.color = new Color(PAUSE.color.r,PAUSE.color.g,PAUSE.color.b,PAUSE.color.a - 0.1f);
				if (ThisIndex.pitch >= 0.1f) {
					ThisIndex.pitch -= 0.1f;
				} else {
					ThisIndex.pitch = 0;
				}
				TableOfPause.color = new Color(TableOfPause.color.r,TableOfPause.color.g,TableOfPause.color.b,TableOfPause.color.a + 0.1f);
				if(TableOfPause.transform.localPosition.x < 0){
				TableOfPause.transform.localPosition = new Vector3 (TableOfPause.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;
				GameObject.Find("Continue").GetComponent<Image> ().enabled = true;
				GameObject.Find("Reset").GetComponent<Image> ().enabled = true;
				GameObject.Find("Menu").GetComponent<Image> ().enabled = true;
				AudioListener.volume = 0;
				PAUSE.enabled = false;
				Time.timeScale = 0;
			}
			yield return new WaitForSeconds(0.005f);
		}
	}

	public void TapToContunie(){
			Time.timeScale = 1;
			AudioListener.volume = 1;
			StartCoroutine ("ProcessToContunie");
	}
	
	IEnumerator ProcessToContunie(){
		PAUSE.enabled = true;
		GameObject.Find("Continue").GetComponent<Image> ().enabled = false;
		GameObject.Find("Reset").GetComponent<Image> ().enabled = false;
		GameObject.Find("Menu").GetComponent<Image> ().enabled = false;
		int Point = 0;
		while(Point == 0){
			if(PAUSE.color.a < 1){
				PAUSE.color = new Color(PAUSE.color.r,PAUSE.color.g,PAUSE.color.b,PAUSE.color.a + 0.1f);
				ThisIndex.pitch += 0.1f;
				TableOfPause.color = new Color(TableOfPause.color.r,TableOfPause.color.g,TableOfPause.color.b,TableOfPause.color.a - 0.1f);
				if(PAUSE.color.a >= 0.5){
					TableOfPause.transform.localPosition = new Vector3 (TableOfPause.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;

				TableOfPause.enabled = false;
			}
			yield return new WaitForSeconds(0.005f);
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
			if(TableOfPause.color.a > 0){
				TableOfPause.color = new Color(TableOfPause.color.r,TableOfPause.color.g,TableOfPause.color.b,TableOfPause.color.a - 0.1f);
				if(TableOfPause.color.a <= 0.5){
					TableOfPause.transform.localPosition = new Vector3 (TableOfPause.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;
				TableOfPause.enabled = false;
			}
			yield return new WaitForSeconds(0.01f);
		}
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
			if(TableOfPause.color.a > 0){
				TableOfPause.color = new Color(TableOfPause.color.r,TableOfPause.color.g,TableOfPause.color.b,TableOfPause.color.a - 0.1f);
				if(TableOfPause.color.a <= 0.5){
					TableOfPause.transform.localPosition = new Vector3 (TableOfPause.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;
				TableOfPause.enabled = false;
			}
			yield return new WaitForSeconds(0.01f);
		}
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

}
