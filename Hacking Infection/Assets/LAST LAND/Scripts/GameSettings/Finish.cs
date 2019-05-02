
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {

    private Image TableOfFinish;
    private Text PAUSE;
    public string NameOfSNext;
    public string NameOfSMenu;
    public AudioClip SounfOfFinish;
    public string NameOfThisScene;

    private AudioSource ThisIndex;
    public bool NoDListener;

    [Header("Pasar a otra Escena")]
    [Range(1, 5)] public float CambioEscena;


	[Header("Other Objects")]
	public Image LoadingIm;
	public Text LoadingTe;

	void Start () {
		ThisIndex = GameObject.Find ("Index").GetComponent<AudioSource> ();
		TableOfFinish = GameObject.Find ("Finish Panel").GetComponent<Image>();
		PAUSE = GameObject.Find ("PAUSE").GetComponent<Text>();
	}

	public IEnumerator ProcessToFinish(){
		GetComponent<AudioSource> ().PlayOneShot (SounfOfFinish);
			int MaxLevel = PlayerPrefs.GetInt ("SavePLine");
			int CurrentLevel = (int.Parse (NameOfThisScene.Substring(1)) + 1);
			if(CurrentLevel > MaxLevel){
				PlayerPrefs.SetInt ("SavePLine", CurrentLevel);
			}

		PlayerPrefs.SetString("Elements","0");

		yield return new WaitForSeconds (1);
		int Point = 0;
		TableOfFinish.enabled = true;
		TableOfFinish.transform.localPosition = new Vector3 (-10,0,0);
		while(Point == 0){
			if(PAUSE.color.a > 0){
				PAUSE.color = new Color(PAUSE.color.r, PAUSE.color.g, PAUSE.color.b, PAUSE.color.a - 0.05f);
				if (!NoDListener) {
					if (ThisIndex.pitch > 0.05f) {
						ThisIndex.pitch -= 0.05f;
					} else {
						ThisIndex.pitch = 0;
					}
				} else {
					ThisIndex.loop = false;
				}
				TableOfFinish.color = new Color(TableOfFinish.color.r, TableOfFinish.color.g, TableOfFinish.color.b, TableOfFinish.color.a + 0.05f);
				if(TableOfFinish.transform.localPosition.x < 0){
					TableOfFinish.transform.localPosition = new Vector3 (TableOfFinish.transform.localPosition.x + 0.5f,0,0);
				}
			}else{
				Point = 1;
				GameObject.Find("Next Finish").GetComponent<Image> ().enabled = true;
				GameObject.Find("Menu Finish").GetComponent<Image> ().enabled = true;
				PAUSE.enabled = false;
				GameObject.Find("Main Camera").GetComponent<MoveCamera> ().enabled = false;
				GameObject.Find("Main Camera").GetComponent<ShotCentre> ().enabled = false;
			}
			yield return new WaitForSeconds(0.01f);
		}
	}

	public void TapToNext(){
		Time.timeScale = 1;
		StartCoroutine ("ProcessToNext");
	}

	IEnumerator ProcessToNext(){
		GameObject.Find("Next Finish").GetComponent<Image> ().enabled = false;
		GameObject.Find("Menu Finish").GetComponent<Image> ().enabled = false;
		int Point = 0;
		while(Point == 0){
			if(TableOfFinish.color.a > 0){
				TableOfFinish.color = new Color(TableOfFinish.color.r, TableOfFinish.color.g, TableOfFinish.color.b, TableOfFinish.color.a - 0.1f);
				if(TableOfFinish.color.a <= 0.5){
					TableOfFinish.transform.localPosition = new Vector3 (TableOfFinish.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;
				TableOfFinish.enabled = false;
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
		SceneManager.LoadScene (NameOfSNext);
	}

//Cambio EscenaProvicional
   public void CambioEscenaPlatfor()
    {
        SceneManager.LoadScene(2);
    }

    public void TapToMenu(){
		Time.timeScale = 1;
		StartCoroutine ("ProcessToMenu");
	}

	IEnumerator ProcessToMenu(){
		GameObject.Find("Next Finish").GetComponent<Image> ().enabled = false;
		GameObject.Find("Menu Finish").GetComponent<Image> ().enabled = false;
		int Point = 0;
		while(Point == 0){
			if(TableOfFinish.color.a > 0){
				TableOfFinish.color = new Color(TableOfFinish.color.r,TableOfFinish.color.g,TableOfFinish.color.b,TableOfFinish.color.a - 0.1f);
				if(TableOfFinish.color.a <= 0.5){
					TableOfFinish.transform.localPosition = new Vector3 (TableOfFinish.transform.localPosition.x + 2f,0,0);
				}
			}else{
				Point = 1;
				TableOfFinish.enabled = false;
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


   

}