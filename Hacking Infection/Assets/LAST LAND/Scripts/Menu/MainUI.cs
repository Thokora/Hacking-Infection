using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour {
	[Header("Main var")]
	public int MaxLvl;
	public Image CirclePart;

	private Image SoundNo;

	private int PositionOfSound = 0;

	[Header("Other Objects")]
	public Image LoadingIm;
	public Text LoadingTe;
	public GameObject RateThis;
	public Sprite RatePanel;
	[Header("Links")]
	public string LinkToInfo = "http://amazeplay.esy.es/";
	public string LinkToGame;


	[Header("For Develop")]
	public bool DelPlayerPrefs;
	public int IfDelThenSavePrefs;


	void Start(){
		PlayerPrefs.SetString ("LastScene","Place Menu");
		AudioListener.volume = 1;
		if(DelPlayerPrefs){
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.SetInt ("SavePLine", IfDelThenSavePrefs);
		}

		SoundNo = GameObject.Find ("SoundNo").GetComponent<Image>();
		if (PlayerPrefs.HasKey ("PositionOfSound")) {
			PositionOfSound = PlayerPrefs.GetInt ("PositionOfSound");
		} else {
			PlayerPrefs.SetInt ("PositionOfSound",0);
		}
		RepeatSCh ();

		// SavePLine
		if (!PlayerPrefs.HasKey ("SavePLine")) {
			PlayerPrefs.SetInt ("SavePLine", 1);
		} else {
			CirclePart.fillAmount =  (float)(PlayerPrefs.GetInt ("SavePLine") - 1) / (float)MaxLvl;
			if (!PlayerPrefs.HasKey ("Rated")) {
				GameObject.Find ("Up Line").GetComponent<Image> ().sprite = RatePanel;
				RateThis.SetActive (true);
			}
		}
	}
		

	void RepeatSCh (){
		if (PositionOfSound == 0) {
			SoundNo.enabled = false;
			AudioListener.pause = false;
		} else {
			SoundNo.enabled = true;
			AudioListener.pause = true;
		}
	}

	public void TouchToExit(){
		Application.Quit ();
	}

	public void TouchToPlay(){
		StartCoroutine ("ToPlay");
	}
		
	public void TouchToInfo(){
		Application.OpenURL (LinkToInfo);
	}

	public void TouchToRate(){
		Application.OpenURL (LinkToGame);
		PlayerPrefs.SetInt ("Rated",0);
	}

	IEnumerator ToPlay(){
			int GetCLvl = PlayerPrefs.GetInt ("SavePLine");
			if (GetCLvl > MaxLvl) {
				GetCLvl = MaxLvl;
			}
			if (GetCLvl <= MaxLvl) {
				GetComponent<AudioSource> ().Play ();
				GameObject.Find ("Black Tr").GetComponent<Animation> ().Play ("BlackTrAClose");
				LoadingIm.gameObject.SetActive (true);
				LoadingTe.gameObject.SetActive (true);
				LoadingIm.enabled = true;
				LoadingTe.enabled = true;
				int Point = 0;
				while (Point == 0) {
					if (LoadingIm.color.a < 1) {
						LoadingIm.color = new Color (1, 1, 1, LoadingIm.color.a + 0.05f);
						LoadingTe.color = new Color (1, 1, 1, LoadingTe.color.a + 0.05f);
					} else {
						Point = 1;
					}
					yield return new WaitForSeconds (0.01f);
				}
				SceneManager.LoadSceneAsync ("Place L" + GetCLvl);
			}
		}


	public void TouchToSound(){
		if (PositionOfSound == 0) {
			PositionOfSound = 1;
		} else {
			PositionOfSound = 0;
		}
		PlayerPrefs.SetInt ("PositionOfSound", PositionOfSound);
		RepeatSCh ();
	}
}