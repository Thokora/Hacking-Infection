using UnityEngine;
using System.Collections;

public class ForRope : MonoBehaviour
{

	private Rigidbody GoRb;
	public LineRenderer SaveLR;

	void Start(){
		GoRb = gameObject.GetComponent<Rigidbody> ();
	}

	public void StartToWork(){
		GoRb.drag = 0;
		GoRb.angularDrag = 0;
		StartCoroutine ("ChangeA");
	}

	IEnumerator ChangeA(){
		SaveLR.SetColors (new Color(0,0.5f,1f),new Color(0,0.5f,1f));
		yield return new WaitForSeconds (1f);
		SaveLR.SetColors (new Color(0,0,0),new Color(0,0,0));
		GoRb.drag = 100;
		GoRb.angularDrag = GoRb.drag;
	}

}

