﻿using UnityEngine;
using System.Collections;

public class CameraFreeRoam : MonoBehaviour {
	
	float rotX, rotY,
	 	  sensitivity = 10f,
 		  speed = 200f;
 	bool _unlocked;

    private Rigidbody rigidbody;

	void OnGUI(){
		GUI.Label(new Rect(10,10,500,500), " Look: Mouse \n Movement: W,A,S,D \n Move Up: Spacebar \n Shoot: Left CTRL \n Show/Hide Mouse: ESC");
	}

	void Start(){
		Screen.lockCursor = true;
        rigidbody = GetComponent<Rigidbody>();

    }

	void Update(){
		if(!_unlocked){
			Screen.lockCursor = true;
			rotX += Input.GetAxis("Mouse X") * sensitivity;
			rotY += Input.GetAxis("Mouse Y") * sensitivity;
			
			rotY = Mathf.Clamp (rotY, -90f, 90); 
			transform.localEulerAngles = new Vector3(-rotY,rotX,0);
		}
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			_unlocked = !_unlocked;
			Screen.lockCursor = false;
		}
	}

	void FixedUpdate () {
		float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
		float jump = Input.GetAxis("Jump") * speed * Time.deltaTime;

		rigidbody.AddForce(transform.forward * vertical);
		rigidbody.AddForce(transform.right * horizontal);
		rigidbody.AddForce(transform.up * jump);

		if(horizontal == 0 && vertical == 0 && jump == 0){
			if(rigidbody.velocity.magnitude > 0)
			{
				rigidbody.velocity *= 0.9f;
			}
		}

	}
}