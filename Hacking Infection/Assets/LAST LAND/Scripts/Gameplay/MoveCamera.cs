﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour {
	[Header("Main")]
	public float SpeedOfPlayer;
	public GameObject i_Player;
	public float MaxDistBS;
	public float PositionOfFinish;
	public float MaxAngleOfRotate;
	public float SpeedOfRotate;

	[Header("Other")]
	public GameObject Step;
	public float HeightOfCam;
	public float DistCamZ;

	private float HighOfStep;
	private float SaveSpeed;
	private int CurrentStep = 1;
	private Vector3 i_PlayerSaved;
	private float DistanceBSteps;

	private int stayrotate = 0;
	private float currentAngle = 0;

	[Header("For Scripts")]
	public bool Finished = false;
	private Transform tI_Player;
	public RectTransform AddThis;
	public Text AddThisText;

    [Header("Fawer Changes")]
    public bool isPlatform = false;
    public bool bossAppear = false;
    public GameObject ctrlPlayer;
    Animator animCtrlPlayer;
    bool backRot;
    
    // Joystick  //
    // Joystick  //

    public LeftJoystick leftJoystick; // the game object containing the LeftJoystick script
    public RightJoystick rightJoystick; // the game object containing the RightJoystick script


    private Vector3 leftJoystickInput; // holds the input of the Left Joystick
    private Vector3 rightJoystickInput; // hold the input of the Right Joystick

    // Joystick  //
    // Joystick  //

    public void Start(){
		SaveSpeed = SpeedOfPlayer;
		HighOfStep = Step.transform.position.y;
		tI_Player = i_Player.transform;

        animCtrlPlayer = ctrlPlayer.GetComponent<Animator>();
	}

	public void RotateRight(){
		stayrotate = 2;
        animCtrlPlayer.SetBool("TurnRight", true);
        animCtrlPlayer.SetBool("TurnLeft", false);
    }
	public void RotateLeft(){
		stayrotate = -2;
        animCtrlPlayer.SetBool("TurnLeft", true);
        animCtrlPlayer.SetBool("TurnRight", false);
    }
	public void RotateEnd(){
		stayrotate = 0;
        animCtrlPlayer.SetBool("TurnRight", false);
        animCtrlPlayer.SetBool("TurnLeft", false);
    }


	void FixedUpdate (){
        if (!isPlatform)
        { 
        if (stayrotate == 0) {
			if (currentAngle > 0.1f) {
				stayrotate = 1;
			}
			if (currentAngle < -0.1f) {
				stayrotate = -1;
			}
		}

		if (stayrotate == 1) {
			if (currentAngle > 0.1f) {
				tI_Player.Rotate (0, -SpeedOfRotate, 0);
				currentAngle -= SpeedOfRotate;
			} else {
				currentAngle = 0f;
				stayrotate = 0;
			}
		}

		if (stayrotate == -1) {
			if (currentAngle < -0.1f) {
				tI_Player.Rotate (0, SpeedOfRotate, 0);
				currentAngle += SpeedOfRotate;
			} else {
				currentAngle = 0f;
				stayrotate = 0;
			}
		}

		if (stayrotate == 2) {
			if (currentAngle < MaxAngleOfRotate) {
				tI_Player.Rotate (0,SpeedOfRotate,0);
				currentAngle += SpeedOfRotate;
			}
		}

		if (stayrotate == -2) {
			if (currentAngle > -MaxAngleOfRotate) {
				tI_Player.Rotate (0,-SpeedOfRotate,0);
				currentAngle -= SpeedOfRotate;
			}
		}
		gameObject.transform.eulerAngles = new Vector3 (gameObject.transform.eulerAngles.x,gameObject.transform.eulerAngles.y,-currentAngle/15);


		if (!Finished) {
			if (tI_Player.position.z + MaxDistBS > PositionOfFinish) {
				Finished = true;
			}
		} else {
			if (SpeedOfPlayer != 0) {
				if (SpeedOfPlayer > SaveSpeed * 0.5f) {
					SpeedOfPlayer = SpeedOfPlayer * 0.99f;
				} else if (SpeedOfPlayer > SaveSpeed * 0.05f) {
					SpeedOfPlayer = SpeedOfPlayer * 0.98f;
				} else {
					SpeedOfPlayer = 0;
					GameObject.Find ("Finish Panel").GetComponent<Finish> ().StartCoroutine ("ProcessToFinish");
				}
			}
		}

		gameObject.transform.position = new Vector3 (tI_Player.position.x, HeightOfCam, tI_Player.position.z - DistCamZ);


            //Linea agregada por fawer

            //#if UNITY_STANDALONE

            //#endif


            if (bossAppear == true)
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        if(!backRot)
                        {
                            RotateRight();
                        }else
                        {
                            RotateLeft();
                        }
                    }

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        if (!backRot)
                        {
                            RotateLeft();
                        }else
                        {
                            RotateRight();
                        }
                    }
                }
                else
                {
                    RotateEnd();
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    backRot = false;
                    tI_Player.Translate(0, 0, SpeedOfPlayer);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    backRot = true;
                    tI_Player.Translate(0, 0, -SpeedOfPlayer);
                }
                else
                {
                    tI_Player.Translate(0, 0, 0);
                }
                
            }else
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        RotateRight();
                    }

                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        RotateLeft();
                    }
                }
                else
                {
                    RotateEnd();
                }

                tI_Player.Translate(0, 0, SpeedOfPlayer);
            }

            // Fin linea agregada por Fawer

		DistanceBSteps += Mathf.Abs (tI_Player.position.z - i_PlayerSaved.z);
		DistanceBSteps += Mathf.Abs (tI_Player.position.x - i_PlayerSaved.x);
		if (DistanceBSteps > MaxDistBS) {
			DistanceBSteps = 0;
			if (i_Player.GetComponent<ForPlayer> ().IsGameOver == 0) {
				//SetSteps ();
			}
		}
		i_PlayerSaved = tI_Player.position;

#if UNITY_ANDROID
        // Joystick  //

        JoystickMovement(); //LINEA AGREGADA POR FAWER

        // Joystick  //
#endif
        }
    }


    void SetSteps(){
		GameObject NewStep = Instantiate (Step);
		NewStep.SetActive (true);
		NewStep.transform.parent = tI_Player;
		NewStep.transform.position = new Vector3 (0,HighOfStep,0);
		NewStep.transform.localPosition = new Vector3 (0,NewStep.transform.localPosition.y,0);
		NewStep.transform.localEulerAngles = new Vector3 (90,0,0);
		NewStep.transform.localScale = new Vector3 (NewStep.transform.localScale.x * CurrentStep,NewStep.transform.localScale.y,NewStep.transform.localScale.z);
		NewStep.transform.parent = tI_Player.parent.transform;
		NewStep.GetComponent<StepsSet> ().StartCoroutine ("StartCount");
		if (CurrentStep == 1) {
			CurrentStep = -1;
		} else {
			CurrentStep = 1;
		}
	}

    // Joystick  //
    // Joystick  //
#if UNITY_ANDROID
    void JoystickMovement() //FUNCIÓN AGREGADA POR FAWER
    {
        // get input from both joysticks
        leftJoystickInput = leftJoystick.GetInputDirection();
        rightJoystickInput = rightJoystick.GetInputDirection();

        float xMovementLeftJoystick = leftJoystickInput.x; // The horizontal movement from joystick 01
        float zMovementLeftJoystick = leftJoystickInput.y; // The vertical movement from joystick 01	

        float xMovementRightJoystick = rightJoystickInput.x; // The horizontal movement from joystick 02
        float zMovementRightJoystick = rightJoystickInput.y; // The vertical movement from joystick 02


        // if there is only input from the left joystick

        if (leftJoystickInput != Vector3.zero && rightJoystickInput == Vector3.zero)
        {

            if (xMovementLeftJoystick > 0.1f)
            {
                RotateRight();
            }
            else if (xMovementLeftJoystick < -0.1f)
            {
                RotateLeft();
            }

        }
        else
        {

            RotateEnd();

        }


    }
#endif
}
