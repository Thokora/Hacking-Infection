using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{

	public LineRenderer LineRope;
	public GameObject[] ArrayPoint;

	private int Point = 0;
	private int Recorder = 0;

	void Start(){
		LineRope.gameObject.SetActive (true);
	}


	void Update(){
		Point = 0;
		Recorder = 0;
		while(Point == 0){
			if(Recorder<ArrayPoint.Length){
				LineRope.SetPosition(Recorder,ArrayPoint[Recorder].transform.position);
				Recorder++;
			}else{
				Point = 1;
			}
		}
	}
}

