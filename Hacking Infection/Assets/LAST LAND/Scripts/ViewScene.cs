using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewScene : MonoBehaviour {

	public float timeStay;
	public string SceneMenu;

	void Start () {
		StartCoroutine ("StartToMenu");
	}

	IEnumerator StartToMenu(){
		Image UsingIm = gameObject.GetComponent<Image> ();
		yield return new WaitForSeconds (timeStay);
		int Point = 0;
		while(Point == 0){
			if (UsingIm.color.a > 0) {
				UsingIm.color = new Color (1, 1, 1, UsingIm.color.a - 0.01f);
			} else {
				Point = 1;
				SceneManager.LoadScene (SceneMenu);
			}
			yield return new WaitForSeconds (0.01f);
		}
	}

}
